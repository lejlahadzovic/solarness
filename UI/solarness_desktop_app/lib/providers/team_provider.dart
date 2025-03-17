import 'dart:convert';

import 'package:solarness_desktop_app/models/Team/team.dart';
import 'package:http/http.dart' as http;
import 'package:solarness_desktop_app/models/TeamMember/team_memeber.dart';

import 'base_provider.dart';

class TeamProvider extends BaseProvider<Team> {
  TeamProvider() : super('Team');

  @override
  Team fromJson(data) {
    // TODO: implement fromJson
    return Team.fromJson(data);
  }
   Future<List<TeamMember>> fetchTeamMembers(int teamId) async {
var url = "${baseUrl}${endPoint}/$teamId/members";
    var uri = Uri.parse(url);

    var headers = createHeaders();

    var response = await http.get(uri, headers: headers);

     if (isValidResponse(response)) {
      var data = jsonDecode(response.body) as List;
      return data.map((e) => TeamMember.fromJson(e)).toList();
    } else {
      throw Exception("Unknown error");
    }
  }
}
