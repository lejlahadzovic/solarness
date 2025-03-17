import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:solarness_mobile/models/Notification/notification.dart';
import 'package:solarness_mobile/models/search_result.dart';
import 'package:solarness_mobile/providers/notification_provider.dart';

class NotificationScreen extends StatefulWidget {
  @override
  _NotificationScreenState createState() => _NotificationScreenState();
}

class _NotificationScreenState extends State<NotificationScreen> {
  bool _isLoading = true;
  bool _hasError = false;
    late NotificationProvider _nProvider;
  SearchResult<Notifications> _notifications = SearchResult();

  @override
  void initState() {
    super.initState();
    _nProvider = context.read<NotificationProvider>();
    _fetchNotifications();
  }

  Future<void> _fetchNotifications() async {
    try {
      var data = await _nProvider.get();
      setState(() {
        _notifications = data;
        _isLoading = false;
      });
    } catch (error) {
      setState(() {
        _isLoading = false;
        _hasError = true;
      });
    }
  }

  String _formatDate(DateTime? date) {
    if (date == null) {
      return "";
    }
    return DateFormat('MMMM dd, yyyy').format(date);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Notifications'),
      ),
      body: _isLoading
          ? Center(child: CircularProgressIndicator())
          : _hasError
              ? Center(child: Text(''))
              : _notifications.result.isEmpty
                  ? Center(child: Text('No new notifications'))
                  : ListView.builder(
                      itemCount: _notifications.result.length,
                      itemBuilder: (ctx, index) {
                        final notification = _notifications.result[index];
                        return Card(
                          margin: const EdgeInsets.symmetric(
                              vertical: 8.0, horizontal: 16.0),
                          child: Padding(
                            padding: const EdgeInsets.all(16.0),
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                Text(
                                  _formatDate(notification.sendDate),
                                  style: TextStyle(
                                    fontSize: 14,
                                    color: Colors.grey[600],
                                  ),
                                ),
                                SizedBox(height: 8),
                                Text(
                                  notification.title ?? 'Nema naziva',
                                  style: TextStyle(
                                    fontSize: 18,
                                    fontWeight: FontWeight.bold,
                                  ),
                                ),
                                SizedBox(height: 8),
                                Text(
                                  notification.content ?? 'Nema dostupnog opisa',
                                  style: TextStyle(
                                    fontSize: 16,
                                    color: Colors.grey[800],
                                  ),
                                ),
                              ],
                            ),
                          ),
                        );
                      },
                    ),
    );
  }
}
