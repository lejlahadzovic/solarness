import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:solarness_desktop_app/login.dart';
import 'package:solarness_desktop_app/models/Task/task.dart';
import 'package:solarness_desktop_app/providers/task_provider.dart';
import 'package:solarness_desktop_app/models/Project/project.dart';
import 'package:solarness_desktop_app/providers/project_provider.dart';
import 'package:solarness_desktop_app/screens/teams_screen.dart';
import 'chat_page.dart';
import 'project_screen.dart';
import 'tasks_screen.dart';
import 'users_list_screen.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      home: const HomePage(),
      theme: ThemeData.light().copyWith(
        scaffoldBackgroundColor: Colors.white,
        primaryColor: Colors.orange,
        appBarTheme: const AppBarTheme(
          backgroundColor: Colors.white,
          iconTheme: IconThemeData(color: Colors.orange),
          titleTextStyle: TextStyle(color: Colors.orange, fontWeight: FontWeight.bold),
        ),
        textTheme: const TextTheme(
          bodyText1: TextStyle(color: Colors.black),
          bodyText2: TextStyle(color: Colors.black),
        ),
        iconTheme: const IconThemeData(color: Colors.orange),
      ),
    );
  }
}

class HomePage extends StatefulWidget {
  const HomePage({super.key});

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  bool isCollapsed = true;
  List<Project> _projects = [];
  List<Task> _tasks = [];
  bool _isLoading = true;
  late ProjectProvider _projectProvider;
  late TaskProvider _taskProvider;

  @override
  void initState() {
    super.initState();
    _projectProvider = context.read<ProjectProvider>();
    _fetchProjects();
    _taskProvider = context.read<TaskProvider>();
    _fetchTasks();
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

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      appBar: AppBar(
        automaticallyImplyLeading: false,
        title: Row(
          children: [
            IconButton(
              icon: Icon(
                isCollapsed ? Icons.menu : Icons.close,
                color: Colors.orange,
              ),
              onPressed: () {
                setState(() {
                  isCollapsed = !isCollapsed;
                });
              },
            ),
            const SizedBox(width: 8),
            const Text(
              'Solarness Dashboard',
              style: TextStyle(
                  fontWeight: FontWeight.bold, color: Colors.orange),
            ),
          ],
        ),
        actions: [
          const SizedBox(width: 8),
          IconButton(
            icon: const Icon(Icons.mail),
            color: Colors.orange,
            onPressed: () {
              Navigator.of(context).push(
                MaterialPageRoute(
                  builder: (context) => UsersListScreen(),
                ),
              );
            },
          ),
        ],
      ),
      body: Row(
        children: [
          // Side Menu
          AnimatedContainer(
            duration: const Duration(milliseconds: 300),
            width: isCollapsed ? 70 : 250,
            color: Colors.white,
            child: Column(
              children: [
                Expanded(
                  child: ListView(
                    children: [
                      _buildMenuItem(
                        icon: Icons.folder,
                        title: 'Projects',
                        isSelected: !isCollapsed,
                        onTap: () {
                          Navigator.of(context).push(
                            MaterialPageRoute(
                              builder: (context) => ProjectPage(),
                            ),
                          );
                        },
                      ),
                      _buildMenuItem(
                        icon: Icons.task,
                        title: 'My Tasks',
                        isSelected: !isCollapsed,
                        onTap: () {
                          Navigator.of(context).push(
                            MaterialPageRoute(
                              builder: (context) => TaskScreen(),
                            ),
                          );
                        },
                      ),
                      _buildMenuItem(
                        icon: Icons.chat,
                        title: 'Chat/Inbox',
                        isSelected: !isCollapsed,
                        onTap: () {
                          Navigator.of(context).push(
                            MaterialPageRoute(
                              builder: (context) => UsersListScreen(),
                            ),
                          );
                        },
                      ),
                      _buildMenuItem(
                        icon: Icons.group,
                        title: 'Teams',
                        isSelected: !isCollapsed,
                        onTap: () {
                          Navigator.of(context).push(
                            MaterialPageRoute(
                              builder: (context) => TeamScreen(),
                            ),
                          );
                        },
                      ),
                      _buildMenuItem(
                        icon: Icons.logout,
                        title: 'Logout',
                        isSelected: !isCollapsed,
                        onTap: () {
                          Navigator.of(context).push(
                            MaterialPageRoute(
                              builder: (context) => LoginPage(),
                            ),
                          );
                        },
                      ),
                    ],
                  ),
                ),
                Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Icon(
                    Icons.wb_sunny_outlined,
                    color: Colors.orange,
                    size: 30,
                  ),
                ),
              ],
            ),
          ),
          // Main Content
          Expanded(
            child: Container(
              padding: const EdgeInsets.all(16.0),
              decoration: const BoxDecoration(
                gradient: LinearGradient(
                  colors: [Colors.white, Colors.white70],
                  begin: Alignment.topCenter,
                  end: Alignment.bottomCenter,
                ),
              ),
              child: SingleChildScrollView(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Welcome to Solarness',
                      style: TextStyle(
                        fontSize: 28,
                        fontWeight: FontWeight.bold,
                        color: Colors.orange,
                      ),
                    ),
                    const SizedBox(height: 10),
                    const Text(
                      'Manage your projects, tasks, and communications efficiently.',
                      style: TextStyle(
                        fontSize: 16,
                        color: Colors.black,
                      ),
                    ),
                    const SizedBox(height: 20),
                    GridView.count(
                      shrinkWrap: true,
                      crossAxisCount:
                          MediaQuery.of(context).size.width < 600 ? 1 : 2,
                      crossAxisSpacing: 16,
                      mainAxisSpacing: 16,
                      childAspectRatio: 1.5,
                      physics: const NeverScrollableScrollPhysics(),
                      children: [
                        _buildDashboardCardWithProjects(
                          title: 'Projects',
                          icon: Icons.folder,
                          projects: _projects,
                        ),
                        _buildDashboardCardWithTasks(
                          title: 'My Tasks',
                          icon: Icons.task,
                          tasks: _tasks,
                        ),
                      ],
                    ),
                  ],
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildMenuItem({
    required IconData icon,
    required String title,
    required bool isSelected,
    required VoidCallback onTap,
  }) {
    return ListTile(
      leading: Icon(
        icon,
        color: isSelected ? Colors.orange : Colors.black54,
        size: 24,
      ),
      title: isCollapsed
          ? null
          : Text(
              title,
              style: TextStyle(
                color: isSelected ? Colors.orange : Colors.black54,
                fontWeight: isSelected ? FontWeight.bold : FontWeight.normal,
              ),
            ),
      tileColor: isSelected ? Colors.orange.shade100 : Colors.transparent,
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(8),
      ),
      onTap: onTap,
    );
  }

  Widget _buildDashboardCardWithProjects({
    required String title,
    required IconData icon,
    required List<Project> projects,
  }) {
    return Card(
      elevation: 4,
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(15),
      ),
      color: Colors.white,
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Row(
              children: [
                Icon(
                  icon,
                  size: 48,
                  color: Colors.orange,
                ),
                const SizedBox(width: 10),
                Text(
                  title,
                  style: const TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.bold,
                    color: Colors.orange,
                  ),
                ),
              ],
            ),
            const SizedBox(height: 10),
            Expanded(
              child: ListView.builder(
                itemCount: projects.length,
                shrinkWrap: true,
                itemBuilder: (context, index) {
                  final project = projects[index];
                  return ListTile(
                    contentPadding: EdgeInsets.zero,
                    title: Text(
                      project.projectName ?? 'No name',
                      style: const TextStyle(
                        color: Colors.black,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                    subtitle: Text(
                      project.projectDescription ?? 'No description',
                      style: const TextStyle(
                        color: Colors.black54,
                      ),
                    ),
                    trailing: IconButton(
                      icon: const Icon(Icons.arrow_forward,
                          color: Colors.orange),
                      onPressed: () {
                        Navigator.of(context).push(
                          MaterialPageRoute(
                            builder: (context) => ProjectPage(project: project),
                          ),
                        );
                      },
                    ),
                  );
                },
              ),
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildDashboardCardWithTasks({
    required String title,
    required IconData icon,
    required List<Task> tasks,
  }) {
    return Card(
      elevation: 4,
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(15),
      ),
      color: Colors.white,
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Row(
              children: [
                Icon(
                  icon,
                  size: 48,
                  color: Colors.orange,
                ),
                const SizedBox(width: 10),
                Text(
                  title,
                  style: const TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.bold,
                    color: Colors.orange,
                  ),
                ),
              ],
            ),
            const SizedBox(height: 10),
            Expanded(
              child: ListView.builder(
                itemCount: tasks.length,
                shrinkWrap: true,
                itemBuilder: (context, index) {
                  final task = tasks[index];
                  return ListTile(
                    contentPadding: EdgeInsets.zero,
                    title: Text(
                      task.taskName ?? 'No name',
                      style: const TextStyle(
                        color: Colors.black,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                    subtitle: Text(
                      task.description ?? 'No description',
                      style: const TextStyle(
                        color: Colors.black54,
                      ),
                    ),
                    trailing: IconButton(
                      icon: const Icon(Icons.arrow_forward,
                          color: Colors.orange),
                      onPressed: () {
                        Navigator.of(context).push(
                          MaterialPageRoute(
                            builder: (context) => TaskScreen(task: task),
                          ),
                        );
                      },
                    ),
                  );
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}
