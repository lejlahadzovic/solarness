import 'dart:io';
import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:dash_chat_2/dash_chat_2.dart';
import 'package:flutter/material.dart';
import 'package:get_it/get_it.dart';
import 'package:provider/provider.dart';
import 'package:solarness_desktop_app/providers/user_provider.dart';
import 'package:solarness_desktop_app/services/database_service.dart';
import 'package:solarness_desktop_app/services/storage_service.dart';
import 'package:solarness_desktop_app/utils/util.dart';
import '../models/Chat/chat.dart';
import '../models/Message/message.dart';
import '../models/User/user.dart';
import '../services/media_service.dart';

class ChatScreen extends StatefulWidget {
  final User chatUser;

  const ChatScreen({super.key, required this.chatUser});

  @override
  State<ChatScreen> createState() => _ChatScreenState();
}

class _ChatScreenState extends State<ChatScreen> {
  final GetIt _getIt = GetIt.instance;
  late User currentStudent;
  late DatabaseService _databaseService;
  late MediaService _mediaService;
  late UserProvider _userProvider;
  late StorageService _storageService;
  ChatUser? currentUser, otherUser;
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    _userProvider = context.read<UserProvider>();
    _databaseService = _getIt.get<DatabaseService>();
    _mediaService = _getIt.get<MediaService>();
    _storageService = _getIt.get<StorageService>();
    _initializeChat();
  }

  Future<void> _initializeChat() async {
    try {
      currentStudent = await _userProvider.getCurrentStudent();
      currentUser = ChatUser(
        id: currentStudent.userId.toString(),
        firstName: currentStudent.firstName,
      );
      otherUser = ChatUser(
        id: widget.chatUser.userId.toString(),
        firstName: widget.chatUser.firstName,
      );
    } catch (e) {
      print('Error initializing chat: $e');
    } finally {
      setState(() {
        _isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white, // White background
      appBar: AppBar(
        title: Text(
          widget.chatUser.firstName!,
          style: TextStyle(color: Color(0xFFFFD700)), // Gold title text
        ),
        backgroundColor: Colors.white, // White app bar background
        elevation: 1,
        iconTheme: IconThemeData(color: Color(0xFFFFD700)), // Gold icon color
      ),
      body: _isLoading ? _buildLoading() : _buildUI(),
    );
  }

  Widget _buildLoading() {
    return Center(child: CircularProgressIndicator());
  }

  Widget _buildUI() {
    return StreamBuilder<DocumentSnapshot<Chat>>(
      stream: _databaseService.getChatData(
        currentStudent.userId.toString(),
        widget.chatUser.userId.toString(),
      ),
      builder: (context, snapshot) {
        if (snapshot.connectionState == ConnectionState.waiting) {
          return _buildLoading();
        }

        if (snapshot.hasError) {
          return Center(child: Text('Error: ${snapshot.error}', style: TextStyle(color: Color(0xFFFFD700))));
        }

        Chat? chat = snapshot.data?.data();
        List<ChatMessage> messages = [];
        if (chat != null && chat.messages != null) {
          messages = _generateChatMessagesList(chat.messages!);
        }

        return DashChat(
          currentUser: currentUser!,
          messages: messages,
          onSend: _sendMessage,
          inputOptions: InputOptions(
            alwaysShowSend: true,
            trailing: [
              _mediaMessageButton(),
            ],
            inputTextStyle: TextStyle(color: Color(0xFFFFD700)), // Gold text color
            inputDecoration: InputDecoration(
              hintText: 'Type your message...',
              hintStyle: TextStyle(color: Color(0xFFFFD700)), // Gold placeholder
              filled: true,
              fillColor: Colors.white, // White input field background
              border: OutlineInputBorder(
                borderRadius: BorderRadius.circular(12),
                borderSide: BorderSide(color: Color(0xFFFFD700)), // Gold border
              ),
            ),
          ),
        );
      },
    );
  }

  Future<void> _sendMessage(ChatMessage chatMessage) async {
    if (chatMessage.medias?.isNotEmpty ?? false) {
      if (chatMessage.medias!.first.type == MediaType.image) {
        Message message = Message(
            senderID: chatMessage.user.id,
            content: chatMessage.medias!.first.url,
            messageType: MessageType.Image,
            sentAt: Timestamp.fromDate(chatMessage.createdAt));
        try {
          await _databaseService.sendChatMessage(
            currentStudent.userId.toString(),
            widget.chatUser.userId.toString(),
            message,
          );
        } catch (e) {
          print('Error sending message: $e');
        }
      }
    } else {
      Message message = Message(
        senderID: currentStudent.userId.toString(),
        content: chatMessage.text,
        messageType: MessageType.Text,
        sentAt: Timestamp.fromDate(chatMessage.createdAt),
      );

      try {
        await _databaseService.sendChatMessage(
          currentStudent.userId.toString(),
          widget.chatUser.userId.toString(),
          message,
        );
      } catch (e) {
        print('Error sending message: $e');
      }
    }
  }

  List<ChatMessage> _generateChatMessagesList(List<Message> messages) {
    List<ChatMessage> chatMessages = messages.map((m) {
      if (m.messageType == MessageType.Image) {
        return ChatMessage(
            user: m.senderID == currentUser!.id ? currentUser! : otherUser!,
            medias: [
              ChatMedia(url: m.content!, fileName: "", type: MediaType.image)
            ],
            createdAt: m.sentAt!.toDate());
      } else {
        return ChatMessage(
          user: m.senderID == currentUser!.id ? currentUser! : otherUser!,
          text: m.content!,
          createdAt: m.sentAt!.toDate(),
        );
      }
    }).toList();
    chatMessages.sort((a, b) {
      return b.createdAt.compareTo(a.createdAt);
    });
    return chatMessages;
  }

  Widget _mediaMessageButton() {
    return IconButton(
      onPressed: () async {
        File? file = await _mediaService.getImageFromGallery();
        if (file != null) {
          String chatID = generateChatID(id1: currentUser!.id, id2: otherUser!.id);
          String? downloadURL = await _storageService.uploadImageToChat(
              file: file, chatID: chatID);
          if (downloadURL != null) {
            ChatMessage chatMessage = ChatMessage(
                user: currentUser!,
                createdAt: DateTime.now(),
                medias: [
                  ChatMedia(
                      url: downloadURL, fileName: "", type: MediaType.image)
                ]);
            _sendMessage(chatMessage);
          }
        }
      },
      icon: Icon(Icons.image, color: Color(0xFFFFD700)), // Gold icon color
    );
  }
}
