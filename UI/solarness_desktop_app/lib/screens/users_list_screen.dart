import 'package:flutter/material.dart';
import 'package:get_it/get_it.dart';
import 'package:provider/provider.dart';
import 'package:solarness_desktop_app/models/User/user.dart';
import 'package:solarness_desktop_app/providers/user_provider.dart';
import 'package:solarness_desktop_app/services/database_service.dart';
import '../screens/chat_page.dart';

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
  User? _selectedUser;

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
        _users = fetchedUsers.result.toList();
        _selectedUser = _users.isNotEmpty ? _users[0] : null; // Default to first user
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
      backgroundColor: Colors.white, // Light white background
      appBar: AppBar(
        backgroundColor: Colors.white, // White AppBar background
        title: Text(
          'Inbox',
          style: TextStyle(
            color: Color(0xFFFFD700), // Softer yellow for title text
            fontWeight: FontWeight.w700,
            fontSize: 24,
          ),
        ),
        elevation: 0,
        iconTheme: IconThemeData(
          color: Color(0xFFFFD700), // Softer yellow for icons
        ),
      ),
      body: _isLoading
          ? Center(
              child: CircularProgressIndicator(
                color: Color(0xFFFFD700), // Softer yellow loading spinner
              ),
            )
          : _hasError
              ? Center(
                  child: Text(
                    'Neuspješno učitavanje podataka. Molimo pokušajte opet.',
                    style: TextStyle(
                      color: Color(0xFFFFD700),
                      fontWeight: FontWeight.w400,
                      fontSize: 16,
                    ),
                  ),
                )
              : _users.isEmpty
                  ? Center(
                      child: Text(
                        'Nema dostupnih podataka.',
                        style: TextStyle(
                          color: Color(0xFFFFD700),
                          fontWeight: FontWeight.w400,
                          fontSize: 16,
                        ),
                      ),
                    )
                  : Row(
                      children: [
                        // User list on the left
                        Container(
                          width: 200, // Adjusted width of the user list
                          padding: EdgeInsets.all(8),
                          color: Color(0xFFF8F8F8), // Light grey background for the user list
                          child: ListView.builder(
                            itemCount: _users.length,
                            itemBuilder: (context, index) {
                              final user = _users[index];
                              return Padding(
                                padding: const EdgeInsets.symmetric(vertical: 4.0, horizontal: 8.0),
                                child: Material(
                                  color: Colors.transparent,
                                  child: InkWell(
                                    onTap: () async {
                                      final chatExists = await _databaseService.checkChatExists(
                                          _currentUser!.userId.toString(), user.userId.toString());
                                      if (!chatExists) {
                                        await _databaseService.createNewChat(
                                            _currentUser!.userId!.toString(), user.userId.toString());
                                      }
                                      setState(() {
                                        _selectedUser = user; 
                                      });
                                    },
                                    borderRadius: BorderRadius.circular(16),
                                    splashColor: Color(0xFFFFD700), // Softer yellow splash color
                                    highlightColor: Colors.transparent,
                                    child: Ink(
                                      decoration: BoxDecoration(
                                        color: _selectedUser?.userId == user.userId
                                            ? Color(0xFFFFF3CD) // Light yellow for selected user
                                            : Color(0xFFF8F8F8),
                                        borderRadius: BorderRadius.circular(16),
                                      ),
                                      child: ListTile(
                                        contentPadding: EdgeInsets.symmetric(vertical: 8, horizontal: 12),
                                        leading: CircleAvatar(
                                          backgroundImage: user.picture != null
                                              ? NetworkImage(user.picture!)
                                              : null,
                                          child: user.picture == null
                                              ? Icon(
                                                  Icons.person,
                                                  color: Color(0xFFFFD700), // Softer yellow icon
                                                )
                                              : null,
                                        ),
                                        title: Text(
                                          user.firstName! + ' ' + user.lastName!,
                                          style: TextStyle(
                                            color: Colors.black, // Black for the title text
                                            fontWeight: FontWeight.w500,
                                            fontSize: 14, // Smaller font size for names
                                          ),
                                        ),
                                      ),
                                    ),
                                  ),
                                ),
                              );
                            },
                          ),
                        ),
                        // Chat screen on the right
                        Expanded(
                          child: _selectedUser != null
                              ? ChatScreen(
                                  chatUser: _selectedUser!,
                                )
                              :  Center(
                                  child: Text(
                                    'Choose user to chat with.',
                                    style: TextStyle(
                                      color: Color(0xFFFFD700),
                                      fontSize: 16,
                                    ),
                                  ),
                                ),
                        ),
                      ],
                    ),
    );
  }
}
