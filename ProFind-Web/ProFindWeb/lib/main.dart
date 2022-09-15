// ignore_for_file: prefer_const_constructors

import 'package:fluent_ui/fluent_ui.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return FluentApp(
      title: 'ProFind Web',
      home: Center(
        child:  NavigationView(
          appBar: NavigationAppBar(title:  Text('XD', style: FluentTheme.of(context).typography.title,)),
          pane: NavigationPane(
            items: [

            ]
          ),
        ),
      ),
    );
  }
}
