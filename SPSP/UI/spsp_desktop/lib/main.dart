// ignore_for_file: prefer_const_constructors

import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:spsp_desktop/providers/cart_provider.dart';
import 'package:spsp_desktop/providers/category_provider.dart';
import 'package:spsp_desktop/providers/menu_item_provider.dart';
import 'package:spsp_desktop/providers/menu_provider.dart';
import 'package:spsp_desktop/providers/order_provider.dart';
import 'package:spsp_desktop/providers/qr_table_provider.dart';
import 'package:spsp_desktop/utils/util.dart';
import './screens/menu_item_list_screen.dart';

void main() {
  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => MenuItemProvider()),
        ChangeNotifierProvider(create: (_) => CategoryProvider()),
        ChangeNotifierProvider(create: (_) => MenuProvider()),
        ChangeNotifierProvider(create: (_) => CartProvider()),
        ChangeNotifierProvider(create: (_) => QRTableProvider()),
        ChangeNotifierProvider(create: (_) => OrderProvider()),
      ],
      child: const MyMaterialApp(),
    ),
  );
}

class MyMaterialApp extends StatelessWidget {
  const MyMaterialApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
        title: "RS2 material app",
        theme: ThemeData(primarySwatch: Colors.blue, useMaterial3: false),
        home: LoginPage());
  }
}

class LoginPage extends StatelessWidget {
  LoginPage({super.key});

  final TextEditingController _usernameController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();
  late MenuItemProvider _menuItemProvider;

  @override
  Widget build(BuildContext context) {
    _menuItemProvider = context.read<MenuItemProvider>();
    return Scaffold(
      appBar: AppBar(title: const Text("Login")),
      body: Center(
        child: Container(
            constraints: const BoxConstraints(maxWidth: 400, maxHeight: 400),
            child: Card(
              child: Padding(
                padding: EdgeInsets.all(16.0),
                child: Column(
                  children: [
                    _buildLogo(),
                    _buildUsernameTextField(_usernameController),
                    const SizedBox(height: 8),
                    _buildPasswordField(_passwordController),
                    const SizedBox(height: 8),
                    _buildLoginButton(context, _usernameController, _passwordController,
                        _menuItemProvider)
                  ],
                ),
              ),
            )),
      ),
    );
  }
}

Widget _buildLogo() {
  // return Image.network("https://cc.fit.ba/Images/logo.png", height: 100, width: 100);
  return Image.asset("assets/images/logo.png", height: 100, width: 100);
}

Widget _buildUsernameTextField(TextEditingController usernameController) {
  return TextField(
    decoration:
        const InputDecoration(labelText: "Username", prefixIcon: Icon(Icons.email)),
    controller: usernameController,
  );
}

Widget _buildPasswordField(TextEditingController passwordController) {
  return TextField(
    obscureText: true,
    enableSuggestions: false,
    autocorrect: false,
    decoration:
        const InputDecoration(labelText: "Password", prefixIcon: Icon(Icons.password)),
    controller: passwordController,
  );
}

Widget _buildLoginButton(BuildContext context, TextEditingController usernameController,
    TextEditingController passwordController, MenuItemProvider menuItemProvider) {
  return ElevatedButton(
      onPressed: () async {
        var username = usernameController.text;
        var password = passwordController.text;

        Authorization.username = username;
        Authorization.password = password;

        try {
          await menuItemProvider.get();

          Navigator.of(context).push(
            MaterialPageRoute(
              builder: (context) => const MenuItemScreen(),
            ),
          );
        } on Exception catch (e) {
          showDialog(
            context: context,
            builder: ((BuildContext context) => AlertDialog(
                  title: Text("Error"),
                  content: Text(
                    e.toString(),
                  ),
                  actions: [
                    TextButton(
                      onPressed: () => Navigator.pop(context),
                      child: Text("Uredu"),
                    ),
                  ],
                )),
          );
        }
      },
      child: const Text("Login"));
}


// class MyApp extends StatelessWidget {
//   const MyApp({super.key});

//   // This widget is the root of your application.
//   @override
//   Widget build(BuildContext context) {
//     return MaterialApp(
//       title: 'Flutter Demo',
//       theme: ThemeData(
//         // This is the theme of your application.
//         //
//         // TRY THIS: Try running your application with "flutter run". You'll see
//         // the application has a purple toolbar. Then, without quitting the app,
//         // try changing the seedColor in the colorScheme below to Colors.green
//         // and then invoke "hot reload" (save your changes or press the "hot
//         // reload" button in a Flutter-supported IDE, or press "r" if you used
//         // the command line to start the app).
//         //
//         // Notice that the counter didn't reset back to zero; the application
//         // state is not lost during the reload. To reset the state, use hot
//         // restart instead.
//         //
//         // This works for code too, not just values: Most code changes can be
//         // tested with just a hot reload.
//         colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
//         useMaterial3: true,
//       ),
//       home: const MyHomePage(title: 'Flutter Demo Home Page'),
//     );
//   }
// }

// class MyHomePage extends StatefulWidget {
//   const MyHomePage({super.key, required this.title});

//   // This widget is the home page of your application. It is stateful, meaning
//   // that it has a State object (defined below) that contains fields that affect
//   // how it looks.

//   // This class is the configuration for the state. It holds the values (in this
//   // case the title) provided by the parent (in this case the App widget) and
//   // used by the build method of the State. Fields in a Widget subclass are
//   // always marked "final".

//   final String title;

//   @override
//   State<MyHomePage> createState() => _MyHomePageState();
// }

// class _MyHomePageState extends State<MyHomePage> {
//   int _counter = 0;

//   void _incrementCounter() {
//     setState(() {
//       // This call to setState tells the Flutter framework that something has
//       // changed in this State, which causes it to rerun the build method below
//       // so that the display can reflect the updated values. If we changed
//       // _counter without calling setState(), then the build method would not be
//       // called again, and so nothing would appear to happen.
//       _counter++;
//     });
//   }

//   @override
//   Widget build(BuildContext context) {
//     // This method is rerun every time setState is called, for instance as done
//     // by the _incrementCounter method above.
//     //
//     // The Flutter framework has been optimized to make rerunning build methods
//     // fast, so that you can just rebuild anything that needs updating rather
//     // than having to individually change instances of widgets.
//     return Scaffold(
//       appBar: AppBar(
//         // TRY THIS: Try changing the color here to a specific color (to
//         // Colors.amber, perhaps?) and trigger a hot reload to see the AppBar
//         // change color while the other colors stay the same.
//         backgroundColor: Theme.of(context).colorScheme.inversePrimary,
//         // Here we take the value from the MyHomePage object that was created by
//         // the App.build method, and use it to set our appbar title.
//         title: Text(widget.title),
//       ),
//       body: Center(
//         // Center is a layout widget. It takes a single child and positions it
//         // in the middle of the parent.
//         child: Column(
//           // Column is also a layout widget. It takes a list of children and
//           // arranges them vertically. By default, it sizes itself to fit its
//           // children horizontally, and tries to be as tall as its parent.
//           //
//           // Column has various properties to control how it sizes itself and
//           // how it positions its children. Here we use mainAxisAlignment to
//           // center the children vertically; the main axis here is the vertical
//           // axis because Columns are vertical (the cross axis would be
//           // horizontal).
//           //
//           // TRY THIS: Invoke "debug painting" (choose the "Toggle Debug Paint"
//           // action in the IDE, or press "p" in the console), to see the
//           // wireframe for each widget.
//           mainAxisAlignment: MainAxisAlignment.center,
//           children: <Widget>[
//             const Text(
//               'You have pushed the button this many times:',
//             ),
//             Text(
//               '$_counter',
//               style: Theme.of(context).textTheme.headlineMedium,
//             ),
//           ],
//         ),
//       ),
//       floatingActionButton: FloatingActionButton(
//         onPressed: _incrementCounter,
//         tooltip: 'Increment',
//         child: const Icon(Icons.add),
//       ), // This trailing comma makes auto-formatting nicer for build methods.
//     );
//   }
// }


//   @override
//   Widget build(BuildContext context) {
//     return MaterialApp(
//       title: "RS2 material app",
//       theme: ThemeData(primarySwatch: Colors.blue, useMaterial3: false),
//       home: Scaffold(
//           appBar: AppBar(
//               title: const Text("Rs 2 tajlt"), backgroundColor: Colors.blue),
//           body: Center(
//               child: Column(
//             mainAxisAlignment: MainAxisAlignment.center,
//             children: [
//               const TextField(
//                   decoration: InputDecoration(labelText: "Enter your name")),
//               const SizedBox(height: 20),
//               ElevatedButton(
//                   onPressed: () {
//                     print("tako ti je tako ti je");
//                   },
//                   child: const Text("Submit"))
//             ],
//           )),
//           floatingActionButton: FloatingActionButton(
//             onPressed: () => {},
//             child: Icon(Icons.add),
//           )),
//     );
//   }
// }