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
  
  @override
  void initState() {
    super.initState();
    _taskProvider = context.read<TaskProvider>();
    _fetchTasks();
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

  void _deleteTask(Task task) {
    _taskProvider.delete(task.taskId!);
    setState(() {
      _tasks.remove(task);
      _selectedTask = null;
    });
  }

  void _editTask(Task task) {
    // Show edit task form (implementation needed)
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.orange,
        title: Text(
          "Tasks",
          style: TextStyle(color: Colors.white),
        ),
      ),
      body: Row(
        children: [
          Expanded(
            flex: 1,
            child: _buildTaskList(),
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
        onPressed: () {}, // Implement add task
        child: Icon(Icons.add),
        backgroundColor: Colors.orange,
      ),
    );
  }

  Widget _buildTaskList() {
    return Container(
      padding: EdgeInsets.all(8),
      child: _tasks.isEmpty
          ? Center(child: CircularProgressIndicator())
          : ListView.builder(
              itemCount: _tasks.length,
              itemBuilder: (context, index) {
                final task = _tasks[index];
                return Card(
                  margin: EdgeInsets.symmetric(vertical: 8, horizontal: 4),
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(12),
                  ),
                  child: ListTile(
                    title: Text(task.taskName ?? "Unnamed Task"),
                    subtitle: Text("Due: ${DateFormat.yMMMd().format(task.endDate!)}"),
                    trailing: Row(
                      mainAxisSize: MainAxisSize.min,
                      children: [
                        IconButton(
                          icon: Icon(Icons.edit, color: Colors.grey),
                          onPressed: () => _editTask(task),
                        ),
                        IconButton(
                          icon: Icon(Icons.delete, color: Colors.red),
                          onPressed: () => _deleteTask(task),
                        ),
                      ],
                    ),
                    onTap: () {
                      setState(() {
                        _selectedTask = task;
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
        style: TextStyle(fontSize: 18),
      ),
    );
  }

  Widget _buildTaskDetails(Task task) {
    return Padding(
      padding: const EdgeInsets.all(16.0),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            task.taskName ?? "Unnamed Task",
            style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
          ),
          SizedBox(height: 16),
          Text("Description: ${task.description ?? 'No description available.'}"),
          SizedBox(height: 16),
          Text("Start Date: ${DateFormat.yMMMd().format(task.startDate!)}"),
          SizedBox(height: 16),
          Text("End Date: ${DateFormat.yMMMd().format(task.endDate!)}"),
          SizedBox(height: 16),
          Text("Status: ${task.status ?? 'No status available.'}"),
        ],
      ),
    );
  }
}