import 'package:flutter/material.dart';
import 'package:profindweb/widgets/navigation_bar.dart';

class AppView extends StatelessWidget {
  final Widget child;

  const AppView({required this.child});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      body: Column(
        children: [NavigationBarWeb(), Expanded(child: child)],
      ),
    );
  }
}
