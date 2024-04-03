import 'package:flutter/material.dart';
import 'package:spsp_desktop/models/menu_item.dart';
import 'package:spsp_desktop/widgets/master_screen.dart';

class MenuItemDetailScreen extends StatefulWidget {
  MenuItem? menuItem;
  MenuItemDetailScreen({super.key, this.menuItem});

  @override
  State<MenuItemDetailScreen> createState() => _MenuItemDetailScreenState();
}

class _MenuItemDetailScreenState extends State<MenuItemDetailScreen> {
  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      title: widget.menuItem?.name ?? "Product details",
      child: const Text("Details"),
    );
  }
}
