import 'package:json_annotation/json_annotation.dart';

part 'projectstatus.g.dart';

@JsonSerializable()
class ProjectStatus {
  int? statusId;
  String? statusName;

  ProjectStatus(this.statusId, this.statusName);

  factory ProjectStatus.fromJson(Map<String, dynamic> json) =>
      _$ProjectStatusFromJson(json);

  Map<String, dynamic> toJson() => _$ProjectStatusToJson(this);
}
