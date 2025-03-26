import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../models/User/user.dart';
import '../providers/user_provider.dart';

class UserScreen extends StatefulWidget {
  @override
  _UserScreenState createState() => _UserScreenState();
}

class _UserScreenState extends State<UserScreen> {
  late UserProvider _userProvider;
  List<User> _users = [];
  List<User> _filteredUsers = [];
  bool _isLoading = true;
  TextEditingController _searchController = TextEditingController();

  @override
  void initState() {
    super.initState();
    _userProvider = context.read<UserProvider>();
    _fetchUsers();
    _searchController.addListener(_filterUsers);
  }

  Future<void> _fetchUsers() async {
    try {
      var users = await _userProvider.get();
      setState(() {
        _users = users.result ?? [];
        _filteredUsers = List.from(_users);
        _isLoading = false;
      });
    } catch (e) {
      print("Error fetching users: $e");
      setState(() => _isLoading = false);
    }
  }

  void _filterUsers() {
    String query = _searchController.text.toLowerCase();
    setState(() {
      _filteredUsers = _users.where((user) {
        return (user.firstName?.toLowerCase().contains(query) ?? false) ||
               (user.lastName?.toLowerCase().contains(query) ?? false);
      }).toList();
    });
  }

  void _editUser(User user) {
    // Navigate to user edit screen
  }

  void _addUser() {
    // Navigate to add new user screen
  }

  void _viewUserDetails(User user) {
    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: Text("User Details"),
          content: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              Text("Name: ${user.firstName} ${user.lastName}"),
              Text("Email: ${user.email}"),
            ],
          ),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: Text("Close"),
            ),
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text("Users")),
      body: Column(
        children: [
          Padding(
            padding: EdgeInsets.all(8.0),
            child: TextField(
              controller: _searchController,
              decoration: InputDecoration(
                labelText: "Search Users",
                border: OutlineInputBorder(),
                prefixIcon: Icon(Icons.search),
              ),
            ),
          ),
          Expanded(
            child: _isLoading
                ? Center(child: CircularProgressIndicator())
                : GridView.builder(
                    padding: EdgeInsets.all(8.0),
                    gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                      crossAxisCount: 3,
                      crossAxisSpacing: 8.0,
                      mainAxisSpacing: 8.0,
                      childAspectRatio: 3 / 2,
                    ),
                    itemCount: _filteredUsers.length,
                    itemBuilder: (context, index) {
                      final user = _filteredUsers[index];
                      return GestureDetector(
                        onTap: () => _viewUserDetails(user),
                        child: Card(
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(6.0),
                          ),
                          child: Padding(
                            padding: EdgeInsets.all(6.0),
                            child: Column(
                              mainAxisAlignment: MainAxisAlignment.center,
                              children: [
                                Icon(Icons.person, size: 50, color: Colors.orangeAccent),
                                SizedBox(height: 4),
                                Text(
                                  "${user.firstName} ${user.lastName}",
                                  style: TextStyle(fontWeight: FontWeight.bold, fontSize: 12),
                                  textAlign: TextAlign.center,
                                ),
                                SizedBox(height: 4),
                                Text(
                                  user.email ?? "No email",
                                  style: TextStyle(fontSize: 10, color: Colors.grey[600]),
                                  textAlign: TextAlign.center,
                                ),
                                 SizedBox(height: 4),
                                Text(
                                  user.role?.name ?? "No email",
                                  style: TextStyle(fontSize: 10, color: Colors.grey[600]),
                                  textAlign: TextAlign.center,
                                ),
                                SizedBox(height: 4),
                                IconButton(
                                  icon: Icon(Icons.edit, color: Colors.orangeAccent, size: 16),
                                  onPressed: () => _editUser(user),
                                ),
                              ],
                            ),
                          ),
                        ),
                      );
                    },
                  ),
          ),
        ],
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _addUser,
        child: Icon(Icons.add),
      ),
    );
  }
}
