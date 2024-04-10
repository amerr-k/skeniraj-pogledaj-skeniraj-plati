import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:spsp_mobile/models/menu.dart';
import 'package:spsp_mobile/models/menu_item.dart';
import 'package:spsp_mobile/providers/cart_provider.dart';
import 'package:spsp_mobile/providers/menu_item_provider.dart';
import 'package:spsp_mobile/providers/order_provider.dart';
import 'package:spsp_mobile/providers/user_provider.dart';
import 'package:spsp_mobile/screens/menu_item_detail_screen.dart';
import 'package:spsp_mobile/screens/menu_item_list_screen.dart';
import 'package:spsp_mobile/utils/util.dart';

void main() => runApp(MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => MenuItemProvider()),
        ChangeNotifierProvider(create: (_) => UserProvider()),
        ChangeNotifierProvider(create: (_) => CartProvider()),
        ChangeNotifierProvider(create: (_) => OrderProvider()),
      ],
      child: MaterialApp(
        debugShowCheckedModeBanner: true,
        theme: ThemeData(
          // Define the default brightness and colors.
          brightness: Brightness.light,
          primaryColor: Colors.deepPurple,
          textButtonTheme: TextButtonThemeData(
              style: TextButton.styleFrom(
                  foregroundColor: Colors.deepPurple,
                  textStyle: const TextStyle(
                      fontSize: 24,
                      fontWeight: FontWeight.bold,
                      fontStyle: FontStyle.italic))),

          // Define the default `TextTheme`. Use this to specify the default
          // text styling for headlines, titles, bodies of text, and more.
          textTheme: const TextTheme(
            headline1: TextStyle(fontSize: 72.0, fontWeight: FontWeight.bold),
            headline6: TextStyle(fontSize: 36.0, fontStyle: FontStyle.italic),
          ),
        ),
        home: HomePage(),
        onGenerateRoute: (settings) {
          if (settings.name == MenuItemListScreen.routeName) {
            return MaterialPageRoute(builder: ((context) => MenuItemListScreen()));
          }
          // else if (settings.name == CartScreen.routeName) {
          //   return MaterialPageRoute(builder: ((context) => CartScreen()));
          // }

          var uri = Uri.parse(settings.name!);
          if (uri.pathSegments.length == 2 &&
              "/${uri.pathSegments.first}" == MenuItemDetailScreen.routeName) {
            var id = uri.pathSegments[1];
            return MaterialPageRoute(
                builder: (context) => MenuItemDetailScreen(
                      id: id,
                      // menuItem: MenuItem(
                      //     1, "name", "description", 1, 10, "code", "image", 1, 1),
                    ));
          }
        },
      ),
    ));

class HomePage extends StatelessWidget {
  TextEditingController _usernameController = TextEditingController();
  TextEditingController _passwordController = TextEditingController();
  late UserProvider _userProvider;
  late MenuItemProvider _menuItemProvider;

  @override
  Widget build(BuildContext context) {
    // _userProvider = Provider.of<UserProvider>(context, listen: false);
    _menuItemProvider = Provider.of<MenuItemProvider>(context, listen: false);

    return Scaffold(
      body: SingleChildScrollView(
        child: Column(
          children: [
            Container(
              height: 400,
              decoration: BoxDecoration(
                  image: DecorationImage(
                      image: AssetImage("assets/images/background.jpg"),
                      fit: BoxFit.fill)),
              child: Stack(children: [
                // Positioned(
                //     left: 110,
                //     top: 0,
                //     width: 90,
                //     height: 120,
                //     child: Container(
                //         decoration: BoxDecoration(
                //             image: DecorationImage(
                //       image: AssetImage("assets/images/light-1.png"),
                //     )))),
                // Positioned(
                //     right: 60,
                //     top: 0,
                //     width: 80,
                //     height: 120,
                //     child: Container(
                //         decoration: BoxDecoration(
                //             image: DecorationImage(
                //       image: AssetImage("assets/images/clock.png"),
                //     )))),
                Container(
                  child: Center(
                    child: Container(
                      child: Padding(
                        padding: EdgeInsets.only(left: 10.0, right: 10.0),
                        child: Text(
                          "SKENIRAJ - PLATI",
                          style: TextStyle(
                              // backgroundColor: Color.fromARGB(155, 226, 227, 240),
                              color: Color.fromARGB(255, 118, 83, 243),
                              fontSize: 40,
                              fontWeight: FontWeight.bold),
                        ),
                      ),
                      decoration: BoxDecoration(
                        color: Color.fromARGB(155, 226, 227, 240), // Background color
                        borderRadius: BorderRadius.circular(10), // Rounded border radius
                      ),
                    ),
                  ),
                )
              ]),
            ),
            Padding(
              padding: EdgeInsets.all(40),
              child: Container(
                decoration: BoxDecoration(
                    color: Colors.white, borderRadius: BorderRadius.circular(10)),
                child: Column(children: [
                  Container(
                    padding: EdgeInsets.all(8),
                    decoration: BoxDecoration(
                        border: Border(bottom: BorderSide(color: Colors.grey))),
                    child: TextField(
                      controller: _usernameController,
                      decoration: InputDecoration(
                          border: InputBorder.none,
                          hintText: "Username",
                          hintStyle: TextStyle(color: Colors.grey[400])),
                    ),
                  ),
                  Container(
                    padding: EdgeInsets.all(8),
                    child: TextField(
                      obscureText: true,
                      controller: _passwordController,
                      decoration: InputDecoration(
                          border: InputBorder.none,
                          hintText: "Pasword",
                          hintStyle: TextStyle(color: Colors.grey[400])),
                    ),
                  ),
                ]),
              ),
            ),
            SizedBox(
              height: 2,
            ),
            Container(
              height: 50,
              margin: EdgeInsets.fromLTRB(40, 0, 40, 0),
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(10),
                gradient: LinearGradient(colors: [
                  Color.fromRGBO(143, 148, 251, 1),
                  Color.fromRGBO(143, 148, 251, .6)
                ]),
              ),
              child: InkWell(
                onTap: () async {
                  try {
                    Authorization.username = _usernameController.text;
                    Authorization.password = _passwordController.text;

                    await _menuItemProvider.get();

                    Navigator.pushNamed(context, MenuItemListScreen.routeName);
                  } catch (e) {
                    showDialog(
                        context: context,
                        builder: (BuildContext context) => AlertDialog(
                              title: Text("Error"),
                              content: Text(e.toString()),
                              actions: [
                                TextButton(
                                  child: Text("Ok"),
                                  onPressed: () => Navigator.pop(context),
                                )
                              ],
                            ));
                  }
                },
                child: Center(child: Text("Login")),
              ),
            ),
            SizedBox(
              height: 40,
            ),
            Text("Forgot password?"),
            SizedBox(
              height: 40,
            ),
          ],
        ),
      ),
    );
  }
}
