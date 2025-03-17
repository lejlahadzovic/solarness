import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:solarness_desktop_app/models/search_result.dart';
import '../models/Team/team.dart';
import '../models/TeamMember/team_memeber.dart';
import '../providers/team_provider.dart';
import '../screens/chat_page.dart'; // Import ChatScreen

class TeamScreen extends StatefulWidget {
  @override
  _TeamScreenState createState() => _TeamScreenState();
}

class _TeamScreenState extends State<TeamScreen> {
  late TeamProvider _teamProvider;
  SearchResult<Team>? _teams;
  List<TeamMember> _teamMembers = [];
  int? _selectedTeamId;
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    _teamProvider = context.read<TeamProvider>();
    _fetchTeams();
  }

  Future<void> _fetchTeams() async {
    try {
      var teams = await _teamProvider.get();
      setState(() {
        _teams = teams;
        _isLoading = false;
      });
    } catch (e) {
      print("Error fetching teams: $e");
      setState(() => _isLoading = false);
    }
  }

  Future<void> _fetchTeamMembers(int teamId) async {
    setState(() => _isLoading = true);
    try {
      var members = await _teamProvider.fetchTeamMembers(teamId);
      setState(() {
        _teamMembers = members;
        _selectedTeamId = teamId;
        _isLoading = false;
      });
    } catch (e) {
      print("Error fetching team members: $e");
      setState(() => _isLoading = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text("Teams")),
      body: Row(
        children: [
          Expanded(
            flex: 2,
            child: _isLoading
                ? Center(child: CircularProgressIndicator())
                : ListView.builder(
                    itemCount: _teams?.count,
                    itemBuilder: (context, index) {
                      final team = _teams?.result[index];
                      return ListTile(
                        title: Text(team?.teamName ?? ''),
                        selected: team?.teamId == _selectedTeamId,
                        onTap: () => _fetchTeamMembers(team!.teamId!),
                      );
                    },
                  ),
          ),
          Expanded(
            flex: 3,
            child: _selectedTeamId == null
                ? Center(child: Text("Select a team to view members"))
                : _isLoading
                    ? Center(child: CircularProgressIndicator())
                    : GridView.builder(
                        padding: EdgeInsets.all(8.0),
                        gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                          crossAxisCount: 3,
                          crossAxisSpacing: 8.0,
                          mainAxisSpacing: 8.0,
                        ),
                        itemCount: _teamMembers.length,
                        itemBuilder: (context, index) {
                          final member = _teamMembers[index];
                          return Card(
                            color: Colors.grey[900],
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(12.0),
                            ),
                            child: Column(
                              mainAxisAlignment: MainAxisAlignment.center,
                              children: [
                                Icon(Icons.person, size: 50, color: Colors.white),
                                SizedBox(height: 8),
                                Text(
                                  member.user?.firstName ?? "",
                                  style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
                                ),
                                SizedBox(height: 4),
                                Text(
                                  member.user?.lastName ?? "",
                                  style: TextStyle(color: Colors.white70),
                                ),
                                SizedBox(height: 8),
                                ElevatedButton(
                                  onPressed: () {
                                    if (member.user != null) {
                                      Navigator.push(
                                        context,
                                        MaterialPageRoute(
                                          builder: (context) => ChatScreen(chatUser: member.user!),
                                        ),
                                      );
                                    }
                                  },
                                  style: ElevatedButton.styleFrom(
                                    backgroundColor: Colors.orange,
                                    shape: RoundedRectangleBorder(
                                      borderRadius: BorderRadius.circular(8.0),
                                    ),
                                  ),
                                  child: Text("Chat"),
                                ),
                              ],
                            ),
                          );
                        },
                      ),
          ),
        ],
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          // Implement team creation logic
        },
        child: Icon(Icons.add),
      ),
    );
  }
}
