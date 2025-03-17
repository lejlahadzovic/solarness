import 'package:solarness_desktop_app/models/Status/projectstatus.dart';

import 'base_provider.dart';

class ProjectStatusProvider extends BaseProvider<ProjectStatus> {
  ProjectStatusProvider() : super('ProjectStatus');

  @override
  ProjectStatus fromJson(data) {
    // TODO: implement fromJson
    return ProjectStatus.fromJson(data);
  }

}
