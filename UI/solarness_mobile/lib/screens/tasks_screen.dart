import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import '../models/Project/project.dart';
import '../models/Task/task.dart';
import '../models/TeamMember/team_memeber.dart';
import '../providers/task_provider.dart';
import '../providers/project_provider.dart';
import '../providers/team_member_provider.dart';

class TaskScreen extends StatefulWidget {
  @override
  _TaskScreenState createState() => _TaskScreenState();
}

class _TaskScreenState extends State<TaskScreen> {
  late TaskProvider _taskProvider;
  late ProjectProvider _projectProvider;
  late TeamMemberProvider _teamMemberProvider;

  List<Task> _tasks = [];
  List<Project> _projects = [];
  List<bool> _taskCompletion = [];
  List<TeamMember> _teamMembers = [];
  bool _isLoading = true;

  @override
  void initState() {
    super.initState();
    _taskProvider = context.read<TaskProvider>();
    _projectProvider = context.read<ProjectProvider>();
    _teamMemberProvider = context.read<TeamMemberProvider>();
    _fetchAllData();
  }

  Future<void> _fetchAllData() async {
    try {
      final results = await Future.wait([
        _taskProvider.get(),
        _projectProvider.get(),
        _teamMemberProvider.get()
      ]);
      final fetchedTasks = results[0].result as List<Task>;

    setState(() {
      _tasks = fetchedTasks;
      _taskCompletion = List.filled(fetchedTasks.length, false);
        _projects = results[1].result as List<Project>;
        _taskCompletion = List.filled(_tasks.length, false);
        _teamMembers = results[2].result as List<TeamMember>;
        _isLoading = false;
      });
    } catch (e) {
      print("Error fetching data: $e");
      
    }
  }

  void _showAddTaskForm() {
    showModalBottomSheet(
      context: context,
      isScrollControlled: true,
      builder: (ctx) {
        return Padding(
          padding: EdgeInsets.only(
              bottom: MediaQuery.of(context).viewInsets.bottom),
          child: TaskForm(
            projects: _projects,
            teamMembers: _teamMembers,
            onTaskAdded: () {
              Navigator.pop(ctx);
              _fetchAllData();
            },
          ),
        );
      },
    );
  }
@override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text("Tasks")),
      body: _isLoading
          ? Center(child: CircularProgressIndicator())
          : ListView.builder(
              itemCount: _tasks.length,
              itemBuilder: (ctx, index) {
                final task = _tasks[index];
                final isCompleted = _taskCompletion[index];
                return Card(
                  margin: EdgeInsets.symmetric(horizontal: 10, vertical: 5),
                  child: ListTile(
                    leading: Checkbox(
                      value: isCompleted,
                      onChanged: (bool? value) {
                        setState(() {
                          _taskCompletion[index] = value ?? false;
                        });
                      },
                    ),
                    title: Text(
                      task.taskName ?? '',
                      style: TextStyle(
                        fontWeight: FontWeight.bold,
                        color: isCompleted ? Colors.green : Colors.black,
                        decoration: isCompleted
                            ? TextDecoration.lineThrough
                            : TextDecoration.none,
                      ),
                    ),
                    subtitle: Text("Due: " +
                        (task.endDate != null
                            ? DateFormat.yMMMd().format(task.endDate!)
                            : "No due date")),
                    trailing: Icon(Icons.arrow_forward_ios),
                    onTap: () => _showTaskDetails(task),
                  ),
                );
              },
            ),
      floatingActionButton: FloatingActionButton(
        onPressed: _showAddTaskForm,
        child: Icon(Icons.add),
      ),
    );
  }

  void _showTaskDetails(Task task) {
    showModalBottomSheet(
      context: context,
      builder: (ctx) {
        return Padding(
          padding: EdgeInsets.all(16),
          child: Column(
            mainAxisSize: MainAxisSize.min,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(task.taskName ?? '',
                  style: TextStyle(
                      fontSize: 18, fontWeight: FontWeight.bold)),
              SizedBox(height: 8),
              Text("Description: ${task.description ?? "No description"}"),
              SizedBox(height: 8),
              Text("Status: ${task.status}"),
              SizedBox(height: 8),
              Text("Start: ${task.startDate != null ? DateFormat.yMMMd().format(task.startDate!) : "N/A"}"),
              Text("End: ${task.endDate != null ? DateFormat.yMMMd().format(task.endDate!) : "N/A"}"),
            ],
          ),
        );
      },
    );
  }
}


class TaskForm extends StatefulWidget {
  final List<Project> projects;
  final List<TeamMember> teamMembers;
  final VoidCallback onTaskAdded;

  TaskForm({required this.projects, required this.teamMembers, required this.onTaskAdded});

  @override
  _TaskFormState createState() => _TaskFormState();
}

class _TaskFormState extends State<TaskForm> {
  final _formKey = GlobalKey<FormState>();
  String? _taskName, _description, _selectedStatus;
  int? _selectedProjectId, _selectedMemberId;
  DateTime? _startDate, _endDate;

  void _submitForm() {
    if (_formKey.currentState!.validate()) {
      _formKey.currentState!.save();

      final taskData = {
        'taskName': _taskName,
        'description': _description,
        'status': _selectedStatus,
        'startDate': _startDate != null ? DateFormat('yyyy-MM-dd').format(_startDate!) : '',
        'endDate': _endDate != null ? DateFormat('yyyy-MM-dd').format(_endDate!) : '',
        'projectId': _selectedProjectId,
        'memberId': _selectedMemberId,
      };

      context.read<TaskProvider>().insert(taskData);
      widget.onTaskAdded();
    }
  }

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.all(16),
      child: Form(
        key: _formKey,
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            TextFormField(
              decoration: InputDecoration(labelText: "Task Name"),
              validator: (value) => value!.isEmpty ? "Enter task name" : null,
              onSaved: (value) => _taskName = value,
            ),
            DropdownButtonFormField<int>(
              decoration: InputDecoration(labelText: "Project"),
              value: _selectedProjectId,
              items: widget.projects
                  .map((p) => DropdownMenuItem(value: p.projectId, child: Text(p.projectName!)))
                  .toList(),
              onChanged: (value) => setState(() => _selectedProjectId = value),
            ),
            ElevatedButton(
              onPressed: _submitForm,
              child: Text("Add Task"),
            )
          ],
        ),
      ),
    );
  }
}
