// ignore_for_file: prefer_const_constructors

import 'package:flutter/material.dart';
import 'package:spsp_mobile/screens/menu_item_list_customer_screen.dart';
import 'package:spsp_mobile/screens/pos_screen.dart';
import 'package:spsp_mobile/screens/qr_code_scanner_customer_screen.dart';

class MasterScreenWidget extends StatefulWidget {
  String? title;
  Widget? titleWidget;
  Widget? child;
  MasterScreenWidget({this.title, this.child, this.titleWidget, super.key});

  @override
  State<MasterScreenWidget> createState() => _MasterScreenWidgetState();
}

class _MasterScreenWidgetState extends State<MasterScreenWidget> {
  int currentIndex = 0;

  void _onItemTapped(int index) async {
    setState(() {
      currentIndex = index;
    });
    if (currentIndex == 0) {
      Navigator.pushNamed(context, MenuItemListCustomerScreen.routeName);
    } else if (currentIndex == 1) {
      // Open camera
      //   final cameras = await availableCameras();
      //   final firstCamera = cameras.last;
      //   Navigator.push(
      //     context,
      //     MaterialPageRoute(
      //       builder: (context) => CameraCustomerScreen(camera: firstCamera),
      //     ),
      //   );
      // }
      Navigator.push(
        context,
        MaterialPageRoute(
          builder: (context) => QRCodeScannerCustomerScreen(),
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: widget.titleWidget ?? Text(widget.title ?? "")),
      drawer: Drawer(
        child: ListView(
          children: [
            ListTile(
              title: Text("Proizvodi"),
              onTap: () {
                Navigator.of(context).push(MaterialPageRoute(
                    builder: (context) => const MenuItemListCustomerScreen()));
              },
            ),
            ListTile(
              title: Text("POS"),
              onTap: () {
                Navigator.of(context)
                    .push(MaterialPageRoute(builder: (context) => POSScreen()));
              },
            ),
            // ListTile(
            //   title: Text("Narudžbe"),
            //   onTap: () {
            //     Navigator.of(context)
            //         .push(MaterialPageRoute(builder: (context) => OrderListScreen()));
            //   },
            // )
          ],
        ),
      ),
      body: widget.child!,
      bottomNavigationBar: BottomNavigationBar(
        items: const <BottomNavigationBarItem>[
          BottomNavigationBarItem(
            icon: Icon(Icons.home),
            label: 'Meni',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.camera),
            label: 'Skeniraj',
          ),
        ],
        selectedItemColor: Colors.amber[800],
        currentIndex: currentIndex,
        onTap: _onItemTapped,
      ),
    );
  }
}
