import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:solarness_desktop_app/models/Project/project.dart';
import 'package:solarness_desktop_app/providers/project_provider.dart';

class ProjectPage extends StatefulWidget {
  final Project? project;

  const ProjectPage({super.key, this.project});
  @override
  _ProjectPageState createState() => _ProjectPageState();
}

class _ProjectPageState extends State<ProjectPage> {
  List<Project> _projects = [];
  bool _isLoading = true;
  Project? _selectedProject;
  late ProjectProvider _projectProvider;

  @override
  void initState() {
    super.initState();
    _projectProvider = context.read<ProjectProvider>();
    _fetchProjects();
    _selectedProject = widget.project;
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

@override
Widget build(BuildContext context) {
  return Scaffold(
    backgroundColor: Color(0xFF1E1E1E),
    appBar: AppBar(
      backgroundColor: Color(0xFF2A2A2A),
      title: Text(
        "Projects",
        style: TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
      ),
      elevation: 4,
      actions: [
        IconButton(
          icon: Icon(Icons.search, color: Color(0xFFFFD700)),
          onPressed: () {
            // Add search functionality
          },
        ),
      ],
    ),
    floatingActionButton: FloatingActionButton(
      onPressed: _showAddProjectDialog,
      backgroundColor: Color(0xFFFFD700),
      child: Icon(Icons.add, color: Colors.black),
    ),
    body: _isLoading
        ? Center(
            child: CircularProgressIndicator(
              color: Color(0xFFFFD700),
            ),
          )
        : Row(
            children: [
              // Projects List
              Container(
                width: 350,
                decoration: BoxDecoration(
                  color: Color(0xFF2A2A2A),
                  boxShadow: [
                    BoxShadow(
                      color: Colors.black.withOpacity(0.5),
                      offset: Offset(0, 5),
                      blurRadius: 10,
                    ),
                  ],
                ),
                child: ListView.builder(
                  padding: EdgeInsets.all(10),
                  itemCount: _projects.length,
                  itemBuilder: (context, index) {
                    final project = _projects[index];
                    return Card(
                      color: Color(0xFF1E1E1E),
                      elevation: 4,
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(12),
                      ),
                      margin: EdgeInsets.only(bottom: 10),
                      child: ListTile(
                        leading: CircleAvatar(
                          backgroundColor: Color(0xFFFFD700),
                          child: Text(
                            (index + 1).toString(),
                            style: TextStyle(color: Colors.black),
                          ),
                        ),
                        title: Text(
                          project.projectName ?? "Unnamed Project",
                          style: TextStyle(
                            color: Colors.white,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                        subtitle: Text(
                          project.city ?? "Unknown City",
                          style: TextStyle(color: Colors.grey),
                        ),
                        onTap: () {
                          setState(() {
                            _selectedProject = project;
                          });
                        },
                      ),
                    );
                  },
                ),
              ),
              // Project Details
              Expanded(
                child: _selectedProject == null
                    ? Center(
                        child: Text(
                          "Select a project to view details",
                          style: TextStyle(color: Colors.grey, fontSize: 18),
                        ),
                      )
                    : Padding(
                        padding: const EdgeInsets.all(16.0),
                        child: SingleChildScrollView(
                          child: Card(
                            color: Color(0xFF2A2A2A),
                            elevation: 6,
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(16),
                            ),
                            child: Padding(
                              padding: EdgeInsets.all(24),
                              child: Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  Text(
                                    _selectedProject!.projectName ?? "Unnamed Project",
                                    style: TextStyle(
                                      color: Color(0xFFFFD700),
                                      fontSize: 28,
                                      fontWeight: FontWeight.bold,
                                    ),
                                  ),
                                  Divider(color: Colors.grey),
                                  SizedBox(height: 10),
                                  _buildDetailRow("Description", _selectedProject!.projectDescription),
                                  _buildDetailRow("City", _selectedProject!.city),
                                  _buildDetailRow("Street Address", _selectedProject!.streetAddress),
                                  _buildDetailRow("KW", "${_selectedProject!.kw ?? 0} KW"),
                                  _buildDetailRow(
                                      "Inspection Date", _selectedProject!.siteInspectionDate?.toString() ?? "N/A"),
                                  _buildDetailRow("Sale Date", _selectedProject!.saleDate?.toString() ?? "N/A"),
                                  _buildDetailRow("Significance", _selectedProject!.significance),
                                  _buildDetailRow("Urgency", _selectedProject!.urgency),
                                  _buildDetailRow("Priority Level", _selectedProject!.priorityLevel),
                                  _buildDetailRow("Completion Date",
                                      _selectedProject!.engineeringSubmitDate?.toString() ?? "Pending"),
                                  _buildDetailRow("Completion Date",
                                      _selectedProject!.engineeringReceivedDate?.toString() ?? "Pending"),
                                ],
                              ),
                            ),
                          ),
                        ),
                      ),
              ),
            ],
          ),
  );
}

  Widget _buildDetailRow(String title, String? value) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 8.0),
      child: Row(
        children: [
          Text(
            "$title: ",
            style: TextStyle(
                color: Color(0xFFFFD700), fontWeight: FontWeight.bold),
          ),
          Expanded(
            child: Text(
              value ?? "N/A",
              style: TextStyle(color: Colors.white),
            ),
          ),
        ],
      ),
    );
  }

  void _showAddProjectDialog() {
    final _formKey = GlobalKey<FormState>();
    String? projectName;
    String? projectDescription;
    String? city;
    String? streetAddress;

    showDialog(
      context: context,
      builder: (BuildContext context) {
        return Dialog(
          shape:
              RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
          backgroundColor: Color(0xFF2A2A2A),
          child: Container(
            width: 600,
            padding: EdgeInsets.all(20),
            child: Form(
              key: _formKey,
              child: Column(
                mainAxisSize: MainAxisSize.min,
                children: [
                  Text(
                    "Add New Project",
                    style: TextStyle(
                        color: Colors.white,
                        fontSize: 20,
                        fontWeight: FontWeight.bold),
                  ),
                  SizedBox(height: 20),
                  ..._buildAddProjectFields(),
                  SizedBox(height: 20),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.end,
                    children: [
                      TextButton(
                        onPressed: () => Navigator.pop(context),
                        child: Text(
                          "Cancel",
                          style: TextStyle(color: Colors.grey),
                        ),
                      ),
                      ElevatedButton(
                        onPressed: () {
                          if (_formKey.currentState!.validate()) {
                            _formKey.currentState!.save();
                            // Add project logic
                          }
                        },
                        style: ElevatedButton.styleFrom(
                            backgroundColor: Color(0xFFFFD700)),
                        child:
                            Text("Add", style: TextStyle(color: Colors.black)),
                      ),
                    ],
                  ),
                ],
              ),
            ),
          ),
        );
      },
    );
  }

  List<Widget> _buildAddProjectFields() {
    return [
      _buildTextField("Project Name",
          (value) => value!.isEmpty ? "Enter project name" : null),
      SizedBox(height: 10),
      _buildTextField("Description", null),
      SizedBox(height: 10),
      _buildTextField("City", null),
      SizedBox(height: 10),
      _buildTextField("Street Address", null),
    ];
  }

  Widget _buildTextField(String label, String? Function(String?)? validator) {
    return TextFormField(
      style: TextStyle(color: Colors.white),
      decoration: InputDecoration(
        labelText: label,
        labelStyle: TextStyle(color: Colors.grey),
        enabledBorder: OutlineInputBorder(
          borderSide: BorderSide(color: Colors.grey),
        ),
      ),
      validator: validator,
      onSaved: (value) {
        // Save logic
      },
    );
  }
}
