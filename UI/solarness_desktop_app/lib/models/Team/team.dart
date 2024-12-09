import 'package:json_annotation/json_annotation.dart';

part 'team.g.dart';

@JsonSerializable()
class Team {
  int? teamId;
  String? teamName;
  String? description;
  DateTime? creationDate;

  Team(this.teamId, this.teamName, this.description, this.creationDate);

  factory Team.fromJson(Map<String, dynamic> json) =>
      _$TeamFromJson(json);

  Map<String, dynamic> toJson() => _$TeamToJson(this);
}
