import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:form_builder_validators/form_builder_validators.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:solarness_desktop_app/models/Project/project.dart';
import 'package:solarness_desktop_app/providers/project_provider.dart';

class ProjectDetailsDialog extends StatefulWidget {
  final Project? project;

  ProjectDetailsDialog({this.project});

  @override
  _ProjectDetailsDialogState createState() => _ProjectDetailsDialogState();
}

class _ProjectDetailsDialogState extends State<ProjectDetailsDialog> {
  final _formKey = GlobalKey<FormBuilderState>();
  late ProjectProvider _projectProvider;

  @override
  void initState() {
    super.initState();
    _projectProvider = context.read<ProjectProvider>();
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
      title: Text(
        widget.project == null ? 'Add Project' : 'Edit Project',
        style: TextStyle(color: Color(0xFFFFD700), fontWeight: FontWeight.bold),
      ),
      contentPadding: EdgeInsets.all(20), // Adds some padding around the content
      content: Container(
        width: 600,  // Set the width to make it wider
        child: SingleChildScrollView(
          child: FormBuilder(
            key: _formKey,
            initialValue: widget.project != null
                ? {
                    'projectName': widget.project!.projectName,
                    'projectDescription': widget.project!.projectDescription,
                    'streetAddress': widget.project!.streetAddress,
                    'city': widget.project!.city,
                    'kw': widget.project!.kw?.toString(),
                    'contractAmount': widget.project!.contractAmount?.toString(),
                    'siteInspectionDate': widget.project!.siteInspectionDate,
                    'engineeringSubmitDate': widget.project!.engineeringSubmitDate,
                    'engineeringReceivedDate': widget.project!.engineeringReceivedDate,
                    'saleDate': widget.project!.saleDate,
                    'significance': widget.project!.significance,
                    'urgency': widget.project!.urgency,
                    'priorityLevel': widget.project!.priorityLevel,
                  }
                : {},
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                // Project Name
                FormBuilderTextField(
                  name: 'projectName',
                  decoration: InputDecoration(
                    labelText: 'Project Name',
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

                // Project Description
                FormBuilderTextField(
                  name: 'projectDescription',
                  decoration: InputDecoration(
                    labelText: 'Project Description',
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

                // Street Address
                FormBuilderTextField(
                  name: 'streetAddress',
                  decoration: InputDecoration(
                    labelText: 'Street Address',
                    labelStyle: TextStyle(color: Color(0xFFFFD700)),
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                  ),
                ),
                SizedBox(height: 10),

                // City
                FormBuilderTextField(
                  name: 'city',
                  decoration: InputDecoration(
                    labelText: 'City',
                    labelStyle: TextStyle(color: Color(0xFFFFD700)),
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                  ),
                ),
                SizedBox(height: 10),

                // KW
                FormBuilderTextField(
                  name: 'kw',
                  decoration: InputDecoration(
                    labelText: 'KW',
                    labelStyle: TextStyle(color: Color(0xFFFFD700)),
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                  ),
                  keyboardType: TextInputType.number,
                  validator: FormBuilderValidators.numeric(),
                ),
                SizedBox(height: 10),

                // Contract Amount
                FormBuilderTextField(
                  name: 'contractAmount',
                  decoration: InputDecoration(
                    labelText: 'Contract Amount',
                    labelStyle: TextStyle(color: Color(0xFFFFD700)),
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color(0xFFFFD700)),
                    ),
                  ),
                  keyboardType: TextInputType.number,
                  validator: FormBuilderValidators.numeric(),
                ),
                SizedBox(height: 10),

                // Date Pickers (Site Inspection, Engineering Submit, Engineering Received, Sale Date)
                _buildDateField('Site Inspection Date', 'siteInspectionDate'),
                _buildDateField('Engineering Submit Date', 'engineeringSubmitDate'),
                _buildDateField('Engineering Received Date', 'engineeringReceivedDate'),
                _buildDateField('Sale Date', 'saleDate'),
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
              var request = Map<String, dynamic>.from(_formKey.currentState!.value);
              request['siteInspectionDate'] = request['siteInspectionDate'] != null
                  ? DateFormat('yyyy-MM-dd').format(request['siteInspectionDate'])
                  : null;
              request['engineeringSubmitDate'] = request['engineeringSubmitDate'] != null
                  ? DateFormat('yyyy-MM-dd').format(request['engineeringSubmitDate'])
                  : null;
              request['engineeringReceivedDate'] = request['engineeringReceivedDate'] != null
                  ? DateFormat('yyyy-MM-dd').format(request['engineeringReceivedDate'])
                  : null;
              request['saleDate'] = request['saleDate'] != null
                  ? DateFormat('yyyy-MM-dd').format(request['saleDate'])
                  : null;

              try {
                if (widget.project == null) {
                  await _projectProvider.insert(request);
                } else {
                  await _projectProvider.update(widget.project!.projectId!, request);
                }
                Navigator.pop(context, true);
                ScaffoldMessenger.of(context).showSnackBar(
                  SnackBar(content: Text('Project saved successfully!')),
                );
              } catch (e) {
                ScaffoldMessenger.of(context).showSnackBar(
                  SnackBar(content: Text('An error occurred: $e')),
                );
              }
            }
          },
          child: Text(widget.project == null ? 'Add' : 'Update'),
          style: ElevatedButton.styleFrom( backgroundColor: Colors.amber),
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
