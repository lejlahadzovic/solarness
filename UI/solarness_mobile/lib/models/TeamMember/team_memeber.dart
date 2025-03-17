import 'package:json_annotation/json_annotation.dart';
import 'package:solarness_mobile/models/Team/team.dart';
import 'package:solarness_mobile/models/User/user.dart';

part 'team_memeber.g.dart';

@JsonSerializable()
class TeamMember {
  int? memberId;
  int? userId;
  int? teamId;
  Team? team;
  User? user;

  TeamMember(this.memberId, this.userId, this.teamId, this.team,this.user);

  factory TeamMember.fromJson(Map<String, dynamic> json) =>
      _$TeamMemberFromJson(json);

  Map<String, dynamic> toJson() => _$TeamMemberToJson(this);
}
