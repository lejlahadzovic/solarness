import 'package:json_annotation/json_annotation.dart';
import 'package:solarness_desktop_app/models/Team/team.dart';
import 'package:solarness_desktop_app/models/Status/projectstatus.dart';

part 'project.g.dart';

@JsonSerializable()
class Project {
  int? projectId;
  String? projectName;
  String? projectDescription;
  String? streetAddress;
  String? city;
  int? kw;
  double? contractAmount;
  DateTime? siteInspectionDate;
  DateTime? engineeringSubmitDate;
  DateTime? engineeringReceivedDate;
  DateTime? saleDate;
  String? significance;
  String? urgency;
  String? priorityLevel;
  int? statusId;
  int? teamId;
  Team? team; 
  ProjectStatus? status;


  Project(this.projectId, this.projectName, this.projectDescription, this.streetAddress, this.city, this.kw,this.siteInspectionDate,this.engineeringSubmitDate,this.engineeringReceivedDate,
  this.saleDate,this.significance,this.urgency,this.priorityLevel,this.statusId,this.teamId, this.status,this.team,this.contractAmount);

  factory Project.fromJson(Map<String, dynamic> json) =>
      _$ProjectFromJson(json);

  Map<String, dynamic> toJson() => _$ProjectToJson(this);
}
