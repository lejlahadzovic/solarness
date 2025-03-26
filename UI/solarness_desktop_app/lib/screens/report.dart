import 'package:flutter/material.dart';
import 'package:fl_chart/fl_chart.dart';
import 'package:provider/provider.dart';
import 'package:solarness_desktop_app/models/Project/project.dart';
import 'package:solarness_desktop_app/providers/project_provider.dart';
import 'package:pdf/widgets.dart' as pw;
import 'package:pdf/pdf.dart';
import 'package:path_provider/path_provider.dart';
import 'dart:io';

class ProjectReportDialog extends StatelessWidget {
  final List<Project> projects;

  const ProjectReportDialog({super.key, required this.projects});

  Future<void> _generatePDF(BuildContext context) async {
    final pdf = pw.Document();

    double totalKW = projects.fold(0, (sum, project) => sum + (project.kw ?? 0));
    int totalProjects = projects.length;
    double totalContractAmount = projects.fold(0, (sum, project) => sum + (project.contractAmount ?? 0));

    pdf.addPage(
      pw.Page(
        build: (pw.Context context) => pw.Column(
          children: [
            pw.Text("Project Report", style: pw.TextStyle(fontSize: 24, fontWeight: pw.FontWeight.bold)),
            pw.SizedBox(height: 20),
            pw.Text("Total Projects: $totalProjects"),
            pw.Text("Total Energy Capacity: $totalKW KW"),
            pw.Text("Total Contract Amount: \$${totalContractAmount.toStringAsFixed(2)}"),
            pw.SizedBox(height: 20),
            ...projects.map((project) => pw.Text("${project.projectName ?? "Unnamed"}: ${project.kw ?? 0} KW, Contract Amount: \$${project.contractAmount?.toStringAsFixed(2) ?? "0.00"}")),
          ],
        ),
      ),
    );

    final directory = "C:/Users/Lejla/Downloads";
    final filePath = "$directory/Project_Report.pdf";
    final file = File(filePath);
    await file.writeAsBytes(await pdf.save());

    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(content: Text("PDF saved to: ${file.path}")),
    );
  }

  @override
  Widget build(BuildContext context) {
    double totalKW = projects.fold(0, (sum, project) => sum + (project.kw ?? 0));
    int totalProjects = projects.length;
    double totalContractAmount = projects.fold(0, (sum, project) => sum + (project.contractAmount ?? 0));

    return AlertDialog(
      title: Text("Project Report"),
      content: SingleChildScrollView(
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            SizedBox(
              height: 400,
              width: 600,
              child: BarChart(
                BarChartData(
                  alignment: BarChartAlignment.spaceAround,
                  barGroups: projects.map((project) {
                    return BarChartGroupData(
                      x: projects.indexOf(project),
                      barRods: [
                        BarChartRodData(
                          toY: project.kw?.toDouble() ?? 0,
                          color: Colors.orange,
                          width: 20,
                        ),
                      ],
                    );
                  }).toList(),
                  titlesData: FlTitlesData(
                    leftTitles: AxisTitles(sideTitles: SideTitles(showTitles: true)),
                    bottomTitles: AxisTitles(
                      sideTitles: SideTitles(
                        showTitles: true,
                        interval: 1,
                        getTitlesWidget: (value, meta) {
                          if (value.toInt() < projects.length) {
                            return Transform.rotate(
                              angle: -0.5,
                              child: Text(projects[value.toInt()].projectName ?? "", style: TextStyle(fontSize: 10)),
                            );
                          }
                          return SizedBox.shrink();
                        },
                      ),
                    ),
                  ),
                ),
              ),
            ),
            SizedBox(height: 20),
            Text("Total Projects: $totalProjects", style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
            Text("Total Energy Capacity: $totalKW KW", style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
            Text("Total Contract Amount: \$${totalContractAmount.toStringAsFixed(2)}", style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
            SizedBox(height: 20),
            ElevatedButton(
              onPressed: () => _generatePDF(context),
              child: Text("Download PDF Report"),
            ),
            SizedBox(height: 20),
            ElevatedButton(
              onPressed: () => Navigator.pop(context),
              child: Text("Close"),
            ),
          ],
        ),
      ),
    );
  }
}

class ProjectPage extends StatefulWidget {
  @override
  _ProjectPageState createState() => _ProjectPageState();
}

class _ProjectPageState extends State<ProjectPage> {
  List<Project> _projects = [];
  bool _isLoading = true;
  late ProjectProvider _projectProvider;

  @override
  void initState() {
    super.initState();
    _projectProvider = context.read<ProjectProvider>();
    _fetchProjects();
  }

  Future<void> _fetchProjects() async {
    try {
      var searchResult = await _projectProvider.get();
      setState(() {
        _projects = searchResult.result;
        _isLoading = false;
      });
    } catch (e) {
      setState(() {
        _isLoading = false;
      });
    }
  }

  void _showReportDialog() {
    showDialog(
      context: context,
      builder: (context) => ProjectReportDialog(projects: _projects),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text("Projects")),
      body: _isLoading
          ? Center(child: CircularProgressIndicator())
          : Column(
              children: [
                Expanded(
                  child: ListView.builder(
                    itemCount: _projects.length,
                    itemBuilder: (context, index) {
                      final project = _projects[index];
                      return ListTile(
                        title: Text(project.projectName ?? "Unnamed"),
                        subtitle: Text("${project.kw ?? 0} KW"),
                      );
                    },
                  ),
                ),
                ElevatedButton(
                  onPressed: _showReportDialog,
                  child: Text("Show Report"),
                ),
              ],
            ),
    );
  }
}
