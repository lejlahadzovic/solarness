import '../models/Task/task.dart';
import 'base_provider.dart';

class TaskProvider extends BaseProvider<Task> {
  TaskProvider() : super('Task');

  @override
  Task fromJson(data) {
    // TODO: implement fromJson
    return Task.fromJson(data);
  }

}
