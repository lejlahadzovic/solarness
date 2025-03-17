import 'package:flutter/material.dart';
import 'package:get_it/get_it.dart';
import 'package:provider/provider.dart';
import 'package:solarness_mobile/providers/notification_provider.dart';
import 'package:solarness_mobile/providers/project_provider.dart';
import 'package:solarness_mobile/providers/task_provider.dart';
import 'package:solarness_mobile/providers/team_member_provider.dart';
import 'package:solarness_mobile/providers/user_provider.dart';
import 'package:solarness_mobile/screens/notifications_screen.dart';
import 'package:solarness_mobile/screens/users_list_screen.dart';
import 'package:solarness_mobile/screens/login_screen.dart';
import 'package:solarness_mobile/screens/profile_screen.dart';
import 'package:solarness_mobile/services/database_service.dart';
import 'package:solarness_mobile/services/media_service.dart';
import 'package:solarness_mobile/utils/util.dart';
import 'screens/main_screen.dart';

import 'services/storage_service.dart';

final navigatorKey=GlobalKey<NavigatorState>();

void main() async {
  final GetIt getIt = GetIt.instance;
  getIt.registerLazySingleton<DatabaseService>(() => DatabaseService());
  getIt.registerLazySingleton<MediaService>(() => MediaService());
  getIt.registerLazySingleton<StorageService>(() => StorageService());
  await setup();
  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => UserProvider()),
        ChangeNotifierProvider(create: (_) => ProjectProvider()),
        ChangeNotifierProvider(create: (_) => TaskProvider()),
        ChangeNotifierProvider(create: (_) => TeamMemberProvider()),
        ChangeNotifierProvider(create: (_) => NotificationProvider()),
      ],
      child: MyApp(),
    ),
  );
}

Future<void> setup() async {
  WidgetsFlutterBinding.ensureInitialized();
  await setupFirebase();
}

class MyApp extends StatelessWidget {
  
  
 const MyApp({super.key});

@override
Widget build(BuildContext context) {
  return MaterialApp(
    title: 'Solarness',
    theme: ThemeData(
      primarySwatch: Colors.orange,
      primaryColor: Colors.orange,
      colorScheme: ColorScheme.fromSeed(
        seedColor: Colors.orange,
        primary: Colors.orange,
        onPrimary: Colors.white,
        secondary: Colors.orangeAccent,
        onSecondary: Colors.black,
      ),
      elevatedButtonTheme: ElevatedButtonThemeData(
        style: ElevatedButton.styleFrom(
          foregroundColor: Colors.white,
          backgroundColor: Colors.orange[300], // Muted orange
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(10), // Rounded corners
          ),
        ),
      ),
      textButtonTheme: TextButtonThemeData(
        style: TextButton.styleFrom(
          foregroundColor: Colors.orange[300], // Muted orange
        ),
      ),
      outlinedButtonTheme: OutlinedButtonThemeData(
        style: OutlinedButton.styleFrom(
          foregroundColor: Colors.orange[300],
          side: BorderSide(color: Colors.orange[300]!),
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(10), // Rounded corners
          ),
        ),
      ),
      inputDecorationTheme: InputDecorationTheme(
        focusedBorder: OutlineInputBorder(
          borderSide: BorderSide(color: Colors.orange[300]!),
        ),
        enabledBorder: OutlineInputBorder(
          borderSide: BorderSide(color: Colors.orange[300]!),
        ),
        labelStyle: TextStyle(color: Colors.grey[400]),
        hintStyle: TextStyle(color: Colors.grey[400]), // Soft hint text color
      ),
      appBarTheme: AppBarTheme(
        backgroundColor: Colors.orange[300], // Muted orange for AppBar
        foregroundColor: Colors.white, // White text
        elevation: 0, // Removing unnecessary shadow
      ),
    ),
      initialRoute: '/',
      navigatorKey: navigatorKey,
      routes: {
        '/': (context) => LoginScreen(),
       // '/profile': (context) => ProfileScreen(),
        '/logout': (context) => LoginScreen(),
        '/chat':(context)=>UsersListScreen(),
        '/obavijesti':(context)=>NotificationScreen(),
      },
    );
  }
}
