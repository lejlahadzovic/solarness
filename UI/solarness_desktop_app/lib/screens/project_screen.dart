import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:solarness_desktop_app/models/Project/project.dart';
import 'package:solarness_desktop_app/providers/project_provider.dart';
import 'package:solarness_desktop_app/screens/project_dialog_screen.dart';

class ProjectPage extends StatefulWidget {
  final Project? project;

  const ProjectPage({super.key, this.project});

  @override
  _ProjectPageState createState() => _ProjectPageState();
}
class _ProjectPageState extends State<ProjectPage> {
  // Existing fields
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

  void _deleteProject() async {
    if (_selectedProject == null) return;
    await _projectProvider.delete(_selectedProject!.projectId);
    setState(() {
      _projects.removeWhere((p) => p.projectId == _selectedProject!.projectId);
      _selectedProject = null;
    });
  }

  // Updated _buildDetailRow to include more fields
  Widget _buildDetailRow(String label, String? value) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 5.0),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            '$label: ',
            style: TextStyle(fontWeight: FontWeight.bold),
          ),
          Expanded(
            child: Text(
              value ?? 'N/A',
              style: TextStyle(color: Colors.black87),
            ),
          ),
        ],
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Color(0xFFF9F9F9),
      appBar: AppBar(
        backgroundColor: Colors.white,
        title: Text(
          "Projects",
          style: TextStyle(
            color: Color(0xFFFFD700),
            fontWeight: FontWeight.bold,
          ),
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
        onPressed: () {
          Navigator.push(
            context,
            MaterialPageRoute(
              builder: (context) => ProjectDetailsDialog(),
            ),
          );
        },
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
               Column(
  children: [
    Expanded(
      child: Container(
        width: 350,
        decoration: BoxDecoration(
          color: Color(0xFFF3F3F3),
          boxShadow: [
            BoxShadow(
              color: Colors.black.withOpacity(0.1),
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
              color: Color(0xFFF9F9F9),
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
                    color: Colors.black,
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
    ),
    SizedBox(height: 10), // Adds spacing before the button
    ElevatedButton.icon(
      onPressed: () async {
        try {
          final file = await _projectProvider.predictEnergy();
          if (file != null) {
            ScaffoldMessenger.of(context).showSnackBar(
              SnackBar(content: Text("File downloaded to: ${file.path}")),
            );
          }
        } catch (e) {
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(content: Text("Download failed: $e")),
          );
        }
      },
      icon: Icon(Icons.download, color: Colors.white),
      label: Text("Download annual energy production report"),
      style: ElevatedButton.styleFrom(backgroundColor: Color(0xFFFFD700)),
    ),
  ],
),
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
                              color: Color(0xFFF3F3F3),
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
                                    _buildDetailRow("Contract Amount", _selectedProject!.contractAmount != null
                                        ? "\$${_selectedProject!.contractAmount!.toStringAsFixed(2)}"
                                        : null),
                                    _buildDetailRow("Site Inspection Date", _selectedProject!.siteInspectionDate != null
                                        ? _selectedProject!.siteInspectionDate!.toLocal().toString().split(' ')[0]
                                        : null),
                                    _buildDetailRow("Engineering Submit Date", _selectedProject!.engineeringSubmitDate != null
                                        ? _selectedProject!.engineeringSubmitDate!.toLocal().toString().split(' ')[0]
                                        : null),
                                    _buildDetailRow("Engineering Received Date", _selectedProject!.engineeringReceivedDate != null
                                        ? _selectedProject!.engineeringReceivedDate!.toLocal().toString().split(' ')[0]
                                        : null),
                                    _buildDetailRow("Sale Date", _selectedProject!.saleDate != null
                                        ? _selectedProject!.saleDate!.toLocal().toString().split(' ')[0]
                                        : null),
                                    _buildDetailRow("Significance", _selectedProject!.significance),
                                    _buildDetailRow("Urgency", _selectedProject!.urgency),
                                    _buildDetailRow("Priority Level", _selectedProject!.priorityLevel),
                                    _buildDetailRow("Status", _selectedProject!.status?.statusName),
                                    _buildDetailRow("Team", _selectedProject!.team?.teamName),

                                    SizedBox(height: 20),
                                    Row(
                                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                      children: [
                                        ElevatedButton.icon(
                                          onPressed: () {
                                            Navigator.push(
                                              context,
                                              MaterialPageRoute(
                                                builder: (context) => ProjectDetailsDialog(project: _selectedProject!),
                                              ),
                                            );
                                          },
                                          icon: Icon(Icons.edit, color: Colors.black),
                                          label: Text("Edit"),
                                          style: ElevatedButton.styleFrom(backgroundColor: Color(0xFFFFD700)),
                                        ),
                                        ElevatedButton.icon(
                                          onPressed: _deleteProject,
                                          icon: Icon(Icons.delete, color: Colors.black),
                                          label: Text("Delete"),
                                          style: ElevatedButton.styleFrom(backgroundColor: Colors.redAccent),
                                        ),
                                      ],
                                    ),
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
}

