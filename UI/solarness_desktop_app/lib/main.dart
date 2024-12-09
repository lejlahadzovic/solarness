import 'package:flutter/material.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:get_it/get_it.dart';
import 'package:provider/provider.dart';
import 'package:solarness_desktop_app/providers/project_provider.dart';
import 'package:solarness_desktop_app/providers/task_provider.dart';
import 'package:solarness_desktop_app/providers/team_member_provider.dart';
import 'package:solarness_desktop_app/providers/user_provider.dart';
import 'firebase_options.dart';
import 'package:solarness_desktop_app/login.dart';

import 'services/database_service.dart';
import 'services/media_service.dart';
import 'services/storage_service.dart';
import 'utils/util.dart';


final navigatorKey=GlobalKey<NavigatorState>();
Future<void> main() async {
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

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        // This is the theme of your application.
        //
        // TRY THIS: Try running your application with "flutter run". You'll see
        // the application has a purple toolbar. Then, without quitting the app,
        // try changing the seedColor in the colorScheme below to Colors.green
        // and then invoke "hot reload" (save your changes or press the "hot
        // reload" button in a Flutter-supported IDE, or press "r" if you used
        // the command line to start the app).
        //
        // Notice that the counter didn't reset back to zero; the application
        // state is not lost during the reload. To reset the state, use hot
        // restart instead.
        //
        // This works for code too, not just values: Most code changes can be
        // tested with just a hot reload.
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.yellow),
        useMaterial3: true,
      ),
      home: LoginPage(),
    );
  }
}
