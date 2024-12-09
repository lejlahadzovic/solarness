import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:solarness_desktop_app/models/TeamMember/team_memeber.dart';
import '../models/Project/project.dart';
import '../models/Task/task.dart';
import '../providers/project_provider.dart';
import '../providers/task_provider.dart';
import 'package:intl/intl.dart';

import '../providers/team_member_provider.dart';

class TaskScreen extends StatefulWidget {
  final Task? task;

  const TaskScreen({super.key, this.task});
  @override
  _TaskScreenState createState() => _TaskScreenState();
}

class _TaskScreenState extends State<TaskScreen> {
  List<Task> _tasks = [];
  bool _isLoading = true;
  Task? _selectedTask;
  late TaskProvider _taskProvider;
  String? _selectedStatus = 'In progress';
  int? _selectedProjectId;
  int? _selectedMemberId;
  DateTime? _startDate;
  DateTime? _endDate;
  List<Project> _projects = [];
  List<TeamMember> _teamMembers = [];
  late ProjectProvider _projectProvider;
  late TeamMemberProvider _teamMemberProvider;

  @override
  void initState() {
    super.initState();
    _taskProvider = context.read<TaskProvider>();
    _fetchTasks();
    _selectedTask = widget.task;
    _projectProvider = context.read<ProjectProvider>();
    _fetchProjects();
    _teamMemberProvider = context.read<TeamMemberProvider>();
    _fetchTeamMember();
  }

  Future<void> _fetchProjects() async {
    try {
      var searchResult = await _projectProvider.get();
      setState(() {
        _projects = searchResult.result;
        _isLoading = false;
      });
    } catch (e) {
      print("Error fetching projects: $e");
      setState(() {
        _isLoading = false;
      });
    }
  }

  Future<void> _fetchTeamMember() async {
    try {
      var searchResult = await _teamMemberProvider.get();
      setState(() {
        _teamMembers = searchResult.result;
        _isLoading = false;
      });
    } catch (e) {
      print("Error fetching team members: $e");
      setState(() {
        _isLoading = false;
      });
    }
  }

  Future<void> _fetchTasks() async {
    try {
      var searchResult = await _taskProvider.get();
      setState(() {
        _tasks = searchResult.result;
        _isLoading = false;
      });
    } catch (e) {
      print("Error fetching tasks: $e");
      setState(() {
        _isLoading = false;
      });
    }
  }

  void _showAddTaskForm(BuildContext context) {
    final _formKey = GlobalKey<FormState>();
    String? taskName;
    String? description;

    showDialog(
      context: context,
      builder: (ctx) {
        return AlertDialog(
          title: Text("Add New Task"),
          content: Form(
            key: _formKey,
            child: SingleChildScrollView(
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  TextFormField(
                    decoration: InputDecoration(labelText: "Task Name"),
                    validator: (value) {
                      if (value == null || value.isEmpty) {
                        return "Please enter a task name.";
                      }
                      return null;
                    },
                    onSaved: (value) {
                      taskName = value;
                    },
                  ),
                  TextFormField(
                    decoration: InputDecoration(labelText: "Description"),
                    onSaved: (value) {
                      description = value;
                    },
                  ),
                  DropdownButtonFormField<int>(
                    decoration: InputDecoration(labelText: "Project"),
                    value: _selectedProjectId,
                    items: _projects
                        .map((project) => DropdownMenuItem<int>(
                              value: project.projectId,
                              child: Text(project.projectName??''),
                            ))
                        .toList(),
                    onChanged: (value) {
                      setState(() {
                        _selectedProjectId = value;
                      });
                    },
                    validator: (value) {
                      if (value == null) {
                        return "Please select a project.";
                      }
                      return null;
                    },
                  ),

                  // Member dropdown picker
                  DropdownButtonFormField<int>(
                    decoration: InputDecoration(labelText: "Member"),
                    value: _selectedMemberId,
                    items: _teamMembers
                        .map((member) => DropdownMenuItem<int>(
                              value: member.memberId,
                              child: Text(member.user?.firstName??' '),
                            ))
                        .toList(),
                    onChanged: (value) {
                      setState(() {
                        _selectedMemberId = value;
                      });
                    },
                    validator: (value) {
                      if (value == null) {
                        return "Please select a member.";
                      }
                      return null;
                    },
                  ),
                  // Status dropdown picker
                  DropdownButtonFormField<String>(
                    decoration: InputDecoration(labelText: "Status"),
                    value: _selectedStatus,
                    items: [
                      DropdownMenuItem(
                          value: 'In progress', child: Text('In progress')),
                      DropdownMenuItem(
                          value: 'Completed', child: Text('Completed')),
                      DropdownMenuItem(
                          value: 'Incomplete', child: Text('Incomplete')),
                    ],
                    onChanged: (value) {
                      setState(() {
                        _selectedStatus = value;
                      });
                    },
                    validator: (value) {
                      if (value == null) {
                        return "Please select a status.";
                      }
                      return null;
                    },
                  ),
                  // Start Date picker
                  TextFormField(
                    decoration: InputDecoration(labelText: "Start Date"),
                    controller: TextEditingController(
                      text: _startDate == null
                          ? ''
                          : DateFormat.yMMMd().format(_startDate!),
                    ),
                    readOnly: true,
                    onTap: () async {
                      DateTime? selectedDate = await showDatePicker(
                        context: context,
                        initialDate: DateTime.now(),
                        firstDate: DateTime(2000),
                        lastDate: DateTime(2101),
                      );
                      if (selectedDate != null) {
                        setState(() {
                          _startDate = selectedDate;
                        });
                      }
                    },
                  ),
                  // End Date picker
                  TextFormField(
                    decoration: InputDecoration(labelText: "End Date"),
                    controller: TextEditingController(
                      text: _endDate == null
                          ? ''
                          : DateFormat.yMMMd().format(_endDate!),
                    ),
                    readOnly: true,
                    onTap: () async {
                      DateTime? selectedDate = await showDatePicker(
                        context: context,
                        initialDate: DateTime.now(),
                        firstDate: DateTime(2000),
                        lastDate: DateTime(2101),
                      );
                      if (selectedDate != null) {
                        setState(() {
                          _endDate = selectedDate;
                        });
                      }
                    },
                  ),
                ],
              ),
            ),
          ),
          actions: [
            TextButton(
              onPressed: () => Navigator.of(ctx).pop(),
              child: Text("Cancel"),
            ),
            ElevatedButton(
              onPressed: () {
                if (_formKey.currentState!.validate()) {
                  _formKey.currentState!.save();

                  // Prepare data for the API request
                  var taskData = {
                    'taskName': taskName,
                    'description': description,
                    'startDate':_startDate != null ? DateFormat('yyyy-MM-dd').format(_startDate!) : '',
                    'endDate': _endDate!= null ? DateFormat('yyyy-MM-dd').format(_startDate!) : '',
                    'status': _selectedStatus,
                    'projectId': _selectedProjectId ?? 0,
                    'memberId': _selectedMemberId ?? 0,
                  };

                  _taskProvider.insert(taskData);
                  Navigator.of(ctx).pop(); // Close the dialog
                }
              },
              child: Text("Add Task"),
            ),
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Color(0xFF1F1F1F),
        title: Text(
          "Tasks",
          style: TextStyle(color: Colors.white),
        ),
      ),
      body: Row(
        children: [
          Expanded(
            flex: 1,
            child: _buildTaskList(context),
          ),
          Expanded(
            flex: 2,
            child: _selectedTask == null
                ? _buildPlaceholder()
                : _buildTaskDetails(_selectedTask!),
          ),
        ],
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () => _showAddTaskForm(context),
        child: Icon(Icons.add),
        backgroundColor: Color(0xFF1F1F1F),
      ),
    );
  }

  Widget _buildTaskList(BuildContext context) {
    return Container(
      color: Color(0xFF2A2A2A),
      child: _tasks.isEmpty
          ? Center(child: CircularProgressIndicator())
          : ListView.builder(
              padding: EdgeInsets.all(8),
              itemCount: _tasks.length,
              itemBuilder: (context, index) {
                final selectedTask = _tasks[index];
                return Card(
                  color: _selectedTask == selectedTask
                      ? Color(0xFF333333)
                      : Color(0xFF2A2A2A),
                  child: ListTile(
                    title: Text(
                      selectedTask.taskName ?? "Unnamed Task",
                      style: TextStyle(color: Colors.white),
                    ),
                    subtitle: Text(
                      "Due: ${DateFormat.yMMMd().format(selectedTask.endDate!)}",
                      style: TextStyle(color: Colors.grey),
                    ),
                    onTap: () {
                      setState(() {
                        _selectedTask = selectedTask;
                      });
                    },
                  ),
                );
              },
            ),
    );
  }

  Widget _buildPlaceholder() {
    return Center(
      child: Text(
        "Select a task to view details",
        style: TextStyle(color: Colors.grey, fontSize: 18),
      ),
    );
  }

  Widget _buildTaskDetails(Task selectedTask) {
    return Container(
      color: Color(0xFF1F1F1F),
      padding: EdgeInsets.all(16),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            "Task Details",
            style: TextStyle(fontSize: 24, color: Colors.white),
          ),
          SizedBox(height: 16),
          Text(
            "Name: ${selectedTask.taskName}",
            style: TextStyle(color: Colors.white),
          ),
          Text(
            "Description: ${selectedTask.description}",
            style: TextStyle(color: Colors.white),
          ),
          Text(
            "Status: ${selectedTask.status}",
            style: TextStyle(color: Colors.white),
          ),
          Text(
            "Start Date: ${DateFormat.yMMMd().format(selectedTask.startDate!)}",
            style: TextStyle(color: Colors.white),
          ),
          Text(
            "End Date: ${DateFormat.yMMMd().format(selectedTask.endDate!)}",
            style: TextStyle(color: Colors.white),
          ),
        ],
      ),
    );
  }
}
