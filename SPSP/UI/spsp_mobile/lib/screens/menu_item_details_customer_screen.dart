import 'dart:convert';
import 'dart:io';

import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:provider/provider.dart';
import 'package:spsp_mobile/models/category.dart';
import 'package:spsp_mobile/models/menu.dart';
import 'package:spsp_mobile/models/menu_item.dart';
import 'package:spsp_mobile/models/search_result.dart';
import 'package:spsp_mobile/providers/category_provider.dart';
import 'package:spsp_mobile/providers/menu_item_provider.dart';
import 'package:spsp_mobile/providers/menu_provider.dart';
import 'package:spsp_mobile/utils/util.dart';
import 'package:spsp_mobile/widgets/master_screen.dart';

// static const String routeName = "/product_details";
// String id;
// ProductDetailsScreen(this.id, {Key? key}) : super(key: key);

class MenuItemDetailsCustomerScreen extends StatefulWidget {
  // MenuItem? menuItem;
  String? id;
  static const String routeName = "/menu_item_details";

  MenuItemDetailsCustomerScreen({super.key, this.id});
  @override
  State<MenuItemDetailsCustomerScreen> createState() =>
      _MenuItemDetailsCustomerScreenState();
}

class _MenuItemDetailsCustomerScreenState extends State<MenuItemDetailsCustomerScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  Map<String, dynamic> _initialValue = {};
  late MenuProvider _menuProvider;
  late MenuItemProvider _menuItemProvider;
  RequestResult<Category>? categoryResult;
  bool isLoading = true;
  String? previewImage;
  MenuItem? menuItemRequestResult;

  @override
  void initState() {
    super.initState();
    _initialValue = {
      'code': menuItemRequestResult?.code,
      'name': menuItemRequestResult?.name,
      'description': menuItemRequestResult?.description,
      'price': menuItemRequestResult?.price?.toString(),
      'inStorage': menuItemRequestResult?.inStorage?.toString(),
      'category': menuItemRequestResult?.category?.name.toString(),
      'menuId': menuItemRequestResult?.menuId?.toString(),
      'image': menuItemRequestResult?.image,
    };

    _menuItemProvider = context.read<MenuItemProvider>();
    _menuProvider = context.read<MenuProvider>();

    initForm();
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
  }

  Future<void> initForm() async {
    if (widget.id != null) {
      var result = await MenuItemProvider().getById(widget.id!);

      if (widget.id != null) {
        var result = await MenuItemProvider().getById(widget.id!);
        setState(() {
          menuItemRequestResult = result;
          isLoading = false;
          _initialValue = {
            'code': menuItemRequestResult?.code,
            'name': menuItemRequestResult?.name,
            'description': menuItemRequestResult?.description,
            'price': menuItemRequestResult?.price?.toString(),
            'inStorage': menuItemRequestResult?.inStorage?.toString(),
            'category': menuItemRequestResult?.category?.name.toString(),
            'menuId': menuItemRequestResult?.menuId?.toString(),
            'image': menuItemRequestResult?.image,
          };
        });
      }

      setState(() {
        menuItemRequestResult = result;
        isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      title: "Product details",
      child: Column(
        children: [
          isLoading
              ? Container()
              : Expanded(
                  child: _buildForm(),
                ),
          Row(
            mainAxisAlignment: MainAxisAlignment.end,
            children: [
              Padding(
                padding: const EdgeInsets.all(10.0),
                child: ElevatedButton(
                    onPressed: () async {
                      Navigator.pop(context);
                    },
                    child: const Text("Natrag")),
              )
            ],
          )
        ],
      ),
    );
  }

  SingleChildScrollView _buildForm() {
    return SingleChildScrollView(
      padding: EdgeInsets.all(16.0),
      child: FormBuilder(
        key: _formKey,
        initialValue: _initialValue,
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            Container(
              width: 300,
              height: 300,
              decoration: BoxDecoration(
                border: Border.all(
                  color: Colors.black,
                  width: 1,
                ),
              ),
              child: menuItemRequestResult?.image != null
                  ? imageFromBase64String(menuItemRequestResult!.image ?? "")
                  : imageFromBase64String(previewImage ?? ""),
            ),
            SizedBox(height: 10),
            FormBuilderTextField(
                decoration: const InputDecoration(
                    labelText: "Naziv", labelStyle: TextStyle(color: Colors.black)),
                name: "name",
                enabled: false,
                style: const TextStyle(color: Colors.black)),
            SizedBox(height: 10),
            FormBuilderTextField(
                decoration: const InputDecoration(
                    labelText: "Opis", labelStyle: TextStyle(color: Colors.black)),
                name: "description",
                enabled: false,
                style: const TextStyle(color: Colors.black)),
            SizedBox(height: 10),
            FormBuilderTextField(
                decoration: const InputDecoration(
                    labelText: "Cijena(BAM)", labelStyle: TextStyle(color: Colors.black)),
                name: "price",
                enabled: false,
                style: const TextStyle(color: Colors.black)),
            SizedBox(height: 10),
            FormBuilderTextField(
                decoration: const InputDecoration(
                    labelText: "Kategorija", labelStyle: TextStyle(color: Colors.black)),
                name: "category",
                enabled: false,
                style: const TextStyle(color: Colors.black)),
          ],
        ),
      ),
    );
  }

  File? _image;
  String? _base64Image;

  Future _getImage() async {
    var result = await FilePicker.platform.pickFiles(type: FileType.image);
    if (result != null && result.files.single.path != null) {
      _image = File(result.files.single.path!);
      _base64Image = base64Encode(_image!.readAsBytesSync());
    }
  }
}
