import '../models/Project/project.dart';
import 'base_provider.dart';

class ProjectProvider extends BaseProvider<Project> {
  ProjectProvider() : super('Project');

  @override
  Project fromJson(data) {
    // TODO: implement fromJson
    return Project.fromJson(data);
  }

}
