import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:form_builder_validators/form_builder_validators.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:solarness_desktop_app/models/Task/task.dart';
import 'package:solarness_desktop_app/providers/task_provider.dart';

class TaskDetailsDialog extends StatefulWidget {
  final Task? task;

  TaskDetailsDialog({this.task});

  @override
  _TaskDetailsDialogState createState() => _TaskDetailsDialogState();
}

class _TaskDetailsDialogState extends State<TaskDetailsDialog> {
  final _formKey = GlobalKey<FormBuilderState>();
  late TaskProvider _taskProvider;

  @override
  void initState() {
    super.initState();
    _taskProvider = context.read<TaskProvider>();
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: Text(
        widget.task == null ? 'Add Task' : 'Edit Task',
        style: TextStyle(color: Color(0xFFFFD700), fontWeight: FontWeight.bold),
      ),
      contentPadding: EdgeInsets.all(20),
      content: Container(
        width: 600,
        child: SingleChildScrollView(
          child: FormBuilder(
            key: _formKey,
            initialValue: widget.task != null
                ? {
                    'taskName': widget.task!.taskName,
                    'description': widget.task!.description,
                    'startDate': widget.task!.startDate,
                    'endDate': widget.task!.endDate,
                    'status': widget.task!.status,
                    'projectId': widget.task!.projectId
                        ?.toString(), // Convert int to String
                    'memberId': widget.task!.memberId?.toString(),
                  }
                : {},
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                // Task Name
                FormBuilderTextField(
                  name: 'taskName',
                  decoration: InputDecoration(
                    labelText: 'Task Name',
                    labelStyle: TextStyle(color: Color(0xFFFFD700)),
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                  ),
                  validator: FormBuilderValidators.required(),
                ),
                SizedBox(height: 10),

                // Task Description
                FormBuilderTextField(
                  name: 'description',
                  decoration: InputDecoration(
                    labelText: 'Task Description',
                    labelStyle: TextStyle(color: Color(0xFFFFD700)),
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                  ),
                  validator: FormBuilderValidators.required(),
                ),
                SizedBox(height: 10),

                // Start Date
                _buildDateField('Start Date', 'startDate'),
                SizedBox(height: 10),

                // End Date
                _buildDateField('End Date', 'endDate'),
                SizedBox(height: 10),

                // Status
                FormBuilderTextField(
                  name: 'status',
                  decoration: InputDecoration(
                    labelText: 'Status',
                    labelStyle: TextStyle(color: Color(0xFFFFD700)),
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                  ),
                  validator: FormBuilderValidators.required(),
                ),
                SizedBox(height: 10),

                // Project ID
                FormBuilderTextField(
                  name: 'projectId',
                  decoration: InputDecoration(
                    labelText: 'Project ID',
                    labelStyle: TextStyle(color: Color(0xFFFFD700)),
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                  ),
                  initialValue: widget.task?.projectId?.toString(),
                  keyboardType: TextInputType.number,
                  validator: FormBuilderValidators.numeric(),
                ),
                SizedBox(height: 10),

                // Member ID
                FormBuilderTextField(
                  name: 'memberId',
                  decoration: InputDecoration(
                    labelText: 'Member ID',
                    labelStyle: TextStyle(color: Color(0xFFFFD700)),
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                  ),
                  initialValue: widget.task?.memberId?.toString(),
                  keyboardType: TextInputType.number,
                  validator: FormBuilderValidators.numeric(),
                ),
              ],
            ),
          ),
        ),
      ),
      actions: [
        TextButton(
          onPressed: () => Navigator.pop(context),
          child: Text('Cancel', style: TextStyle(color: Colors.black)),
        ),
        ElevatedButton(
          onPressed: () async {
            if (_formKey.currentState!.saveAndValidate()) {
              var request =
                  Map<String, dynamic>.from(_formKey.currentState!.value);
              request['startDate'] = request['startDate'] != null
                  ? DateFormat('yyyy-MM-dd').format(request['startDate'])
                  : null;
              request['endDate'] = request['endDate'] != null
                  ? DateFormat('yyyy-MM-dd').format(request['endDate'])
                  : null;
              request['projectId'] = request['projectId'] != null
                  ? int.tryParse(request['projectId'])
                  : null;
              request['memberId'] = request['memberId'] != null
                  ? int.tryParse(request['memberId'])
                  : null;

              try {
                if (widget.task == null) {
                  await _taskProvider.insert(request);
                } else {
                  await _taskProvider.update(widget.task!.taskId!, request);
                }
                Navigator.pop(context, true);
                ScaffoldMessenger.of(context).showSnackBar(
                  SnackBar(content: Text('Task saved successfully!')),
                );
              } catch (e) {
                ScaffoldMessenger.of(context).showSnackBar(
                  SnackBar(content: Text('An error occurred: $e')),
                );
              }
            }
          },
          child: Text(widget.task == null ? 'Add' : 'Update'),
          style: ElevatedButton.styleFrom(backgroundColor: Colors.amber),
        ),
      ],
    );
  }

  // Helper method to create date picker fields
  Widget _buildDateField(String label, String name) {
    return FormBuilderDateTimePicker(
      name: name,
      inputType: InputType.date,
      format: DateFormat('yyyy-MM-dd'),
      decoration: InputDecoration(
        labelText: label,
        labelStyle: TextStyle(color: Color(0xFFFFD700)),
        enabledBorder: OutlineInputBorder(
          borderSide: BorderSide(color: Color(0xFFFFD700)),
        ),
        focusedBorder: OutlineInputBorder(
          borderSide: BorderSide(color: Color(0xFFFFD700)),
        ),
      ),
    );
  }
}
