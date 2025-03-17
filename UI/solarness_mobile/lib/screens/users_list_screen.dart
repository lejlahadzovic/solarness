import 'package:flutter/material.dart';
import 'package:get_it/get_it.dart';
import 'package:provider/provider.dart';
import 'package:solarness_mobile/models/User/user.dart';
import 'package:solarness_mobile/providers/user_provider.dart';
import 'package:solarness_mobile/screens/chat_page.dart';
import '../services/database_service.dart';

class UsersListScreen extends StatefulWidget {
  @override
  _UsersListScreenState createState() => _UsersListScreenState();
}

class _UsersListScreenState extends State<UsersListScreen> {
  final GetIt _getIt = GetIt.instance;
  late UserProvider _userProvider;
  late DatabaseService _databaseService;
  List<User> _users = [];
  bool _isLoading = true;
  bool _hasError = false;
  User? _currentUser;

  @override
  void initState() {
    super.initState();
    _userProvider = context.read<UserProvider>();
    _databaseService = _getIt.get<DatabaseService>();
    _fetchUsers();
  }

  Future<void> _fetchUsers() async {
    try {
      var fetchedUsers = await _userProvider.get();
      var currentUser = await _userProvider.getCurrentStudent();

      setState(() {
        _currentUser = currentUser;
        _users = fetchedUsers.result
            .where((user) => user.userId != _currentUser?.userId)
            .toList();
        _isLoading = false;
      });
    } catch (e) {
      print("Error fetching users: $e");
      setState(() {
        _hasError = true;
        _isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Inbox'),
      ),
      body: _isLoading
          ? Center(child: CircularProgressIndicator())
          : _hasError
              ? Center(child: Text('Neuspješno učitavanje podataka. Molimo pokušajte opet.'))
              : _users.isEmpty
                  ? Center(child: Text('Nema dostupnih podataka.'))
                  : ListView.builder(
                      itemCount: _users.length,
                      itemBuilder: (context, index) {
                        final user = _users[index];
                        return ListTile(
                          leading: CircleAvatar(
                            child: Icon(Icons.person)
                                
                          ),
                          title: Text(user.firstName! +
                              ' ' +
                              user.lastName!),
                          subtitle: Text(user.email!),
                          onTap: () async {
                            final chatExists =
                                await _databaseService.checkChatExists(
                                    _currentUser!.userId!.toString(),
                                    user.userId.toString());
                            print(chatExists);
                            if (!chatExists) {
                              await _databaseService.createNewChat(
                                  _currentUser!.userId!.toString(),
                                  user.userId.toString());
                            }
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                builder: (context) => ChatScreen(
                                  chatUser: user,
                                ),
                              ),
                            );
                          },
                        );
                      },
                    ),
    );
  }
}
