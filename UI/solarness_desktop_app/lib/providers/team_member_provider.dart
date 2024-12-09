import 'package:solarness_desktop_app/models/TeamMember/team_memeber.dart';

import 'base_provider.dart';

class TeamMemberProvider extends BaseProvider<TeamMember> {
  TeamMemberProvider() : super('TeamMember');

  @override
  TeamMember fromJson(data) {
    // TODO: implement fromJson
    return TeamMember.fromJson(data);
  }

}
