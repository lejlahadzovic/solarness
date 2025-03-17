import 'package:json_annotation/json_annotation.dart';
import 'package:solarness_mobile/models/TeamMember/team_memeber.dart';

import '../Project/project.dart';

part 'task.g.dart';

@JsonSerializable()
class Task {
  int? taskId;
  int? projectId;
  String? taskName;
  String? description;
  DateTime? startDate;
  DateTime? endDate;
  String? status;
  int? memberId;
  Project? project;
  TeamMember? member;


  Task(this.taskId, this.projectId, this.taskName, this.description, this.startDate, this.endDate, this.status,);

  factory Task.fromJson(Map<String, dynamic> json) =>
      _$TaskFromJson(json);

  Map<String, dynamic> toJson() => _$TaskToJson(this);
}
