import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:solarness_mobile/models/Task/task.dart';
import 'package:solarness_mobile/providers/task_provider.dart';
import 'package:solarness_mobile/models/Project/project.dart';
import 'package:solarness_mobile/providers/project_provider.dart';
import 'chat_page.dart';
import 'notifications_screen.dart';
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
    return MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (context) => ProjectProvider()),
        ChangeNotifierProvider(create: (context) => TaskProvider()),
      ],
      child: MaterialApp(
        debugShowCheckedModeBanner: false,
        theme: ThemeData.light().copyWith(
          scaffoldBackgroundColor: Colors.white,
          primaryColor: Colors.blue,
          appBarTheme: const AppBarTheme(
            backgroundColor: Colors.blue,
            foregroundColor: Colors.white,
          ),
        ),
        home: const HomePage(),
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
  int _selectedIndex = 0;

  final List<Widget> _screens = [
    const DashboardScreen(),
    ProjectPage(),
    TaskScreen(),
    UsersListScreen(),
    NotificationScreen(),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: _screens[_selectedIndex],
      bottomNavigationBar: BottomNavigationBar(
        backgroundColor: Colors.white,
        selectedItemColor: Colors.orangeAccent,
        unselectedItemColor: Colors.grey,
        currentIndex: _selectedIndex,
        onTap: (index) {
          setState(() => _selectedIndex = index);
        },
        items: const [
          BottomNavigationBarItem(icon: Icon(Icons.dashboard), label: 'Home'),
          BottomNavigationBarItem(icon: Icon(Icons.folder), label: 'Projects'),
          BottomNavigationBarItem(icon: Icon(Icons.task), label: 'Tasks'),
          BottomNavigationBarItem(icon: Icon(Icons.chat), label: 'Chat'),
          BottomNavigationBarItem(icon: Icon(Icons.notifications), label: 'Notifications'),
        ],
      ),
    );
  }
}

class DashboardScreen extends StatefulWidget {
  const DashboardScreen({super.key});

  @override
  State<DashboardScreen> createState() => _DashboardScreenState();
}

class _DashboardScreenState extends State<DashboardScreen> {
  List<Project> _projects = [];
  List<Task> _tasks = [];
  bool _isLoading = true;
  String? _error;

  @override
  void initState() {
    super.initState();
    _fetchData();
  }

  Future<void> _fetchData() async {
    try {
      final projectProvider = context.read<ProjectProvider>();
      final taskProvider = context.read<TaskProvider>();

      var projectResult = await projectProvider.get();
      var taskResult = await taskProvider.get();

      setState(() {
        _projects = projectResult.result;
        _tasks = taskResult.result;
        _isLoading = false;
      });
    } catch (e) {
      setState(() {
        _error = "Error fetching data. Please try again later.";
        _isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(16.0),
      child: _isLoading
          ? const Center(child: CircularProgressIndicator())
          : _error != null
              ? Center(child: Text(_error!, style: const TextStyle(color: Colors.red)))
              : Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    const Text(
                      'Welcome to Solarness',
                      style: TextStyle(
                        fontSize: 24,
                        fontWeight: FontWeight.bold,
                        color: Colors.black,
                      ),
                    ),
                    const SizedBox(height: 10),
                    const Text(
                      'Manage your projects and tasks efficiently.',
                      style: TextStyle(fontSize: 16, color: Colors.black54),
                    ),
                    const SizedBox(height: 20),
                    Expanded(
                      child: ListView(
                        children: [
                          const Text(
                            'Projects',
                            style: TextStyle(
                              fontSize: 20,
                              fontWeight: FontWeight.bold,
                              color: Colors.orangeAccent,
                            ),
                          ),
                          const SizedBox(height: 10),
                          _projects.isEmpty
                              ? const Text('No projects available', style: TextStyle(color: Colors.black54))
                              : Column(
                                  children: _projects
                                      .map((project) => Card(
                                            color: Colors.white,
                                            elevation: 3,
                                            child: ListTile(
                                              title: Text(
                                                project.projectName ?? 'No name',
                                                style: const TextStyle(color: Colors.black),
                                              ),
                                              subtitle: Text(
                                                project.projectDescription ?? 'No description',
                                                style: const TextStyle(color: Colors.black54),
                                              ),
                                            ),
                                          ))
                                      .toList(),
                                ),
                          const SizedBox(height: 20),
                          const Text(
                            'Tasks',
                            style: TextStyle(
                              fontSize: 20,
                              fontWeight: FontWeight.bold,
                              color: Colors.orangeAccent,
                            ),
                          ),
                          const SizedBox(height: 10),
                          _tasks.isEmpty
                              ? const Text('No tasks available', style: TextStyle(color: Colors.black54))
                              : Column(
                                  children: _tasks
                                      .map((task) => Card(
                                            color: Colors.white,
                                            elevation: 3,
                                            child: ListTile(
                                              title: Text(
                                                task.taskName ?? 'No name',
                                                style: const TextStyle(color: Colors.black),
                                              ),
                                              subtitle: Text(
                                                task.status ?? 'No status',
                                                style: const TextStyle(color: Colors.black54),
                                              ),
                                            ),
                                          ))
                                      .toList(),
                                ),
                        ],
                      ),
                    ),
                  ],
                ),
    );
  }
}