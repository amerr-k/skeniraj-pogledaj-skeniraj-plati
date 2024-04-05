import 'dart:convert';
import 'dart:io';

import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:provider/provider.dart';
import 'package:spsp_desktop/models/category.dart';
import 'package:spsp_desktop/models/menu.dart';
import 'package:spsp_desktop/models/menu_item.dart';
import 'package:spsp_desktop/models/search_result.dart';
import 'package:spsp_desktop/providers/category_provider.dart';
import 'package:spsp_desktop/providers/menu_item_provider.dart';
import 'package:spsp_desktop/providers/menu_provider.dart';
import 'package:spsp_desktop/utils/util.dart';
import 'package:spsp_desktop/widgets/master_screen.dart';

class MenuItemDetailScreen extends StatefulWidget {
  MenuItem? menuItem;

  MenuItemDetailScreen({super.key, this.menuItem});
  @override
  State<MenuItemDetailScreen> createState() => _MenuItemDetailScreenState();
}

class _MenuItemDetailScreenState extends State<MenuItemDetailScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  Map<String, dynamic> _initialValue = {};
  late CategoryProvider _categoryProvider;
  late MenuProvider _menuProvider;
  late MenuItemProvider _menuItemProvider;
  SearchResult<Category>? categoryResult;
  SearchResult<Menu>? menuResult;
  bool isLoading = true;
  String? previewImage;

  @override
  void initState() {
    super.initState();
    _initialValue = {
      'code': widget.menuItem?.code,
      'name': widget.menuItem?.name,
      'description': widget.menuItem?.description,
      'price': widget.menuItem?.price?.toString(),
      'inStorage': widget.menuItem?.inStorage?.toString(),
      'categoryId': widget.menuItem?.categoryId?.toString(),
      'menuId': widget.menuItem?.menuId?.toString(),
      'image': widget.menuItem?.image,
    };

    _categoryProvider = context.read<CategoryProvider>();
    _menuItemProvider = context.read<MenuItemProvider>();
    _menuProvider = context.read<MenuProvider>();

    initForm();
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();

    // _categoryProvider = context.read<CategoryProvider>();

    // var data = await _categoryProvider.get();
    // setState(() {
    //   searchResult = data;
    // });

    // if (widget.menuItem != null) {
    //   setState(() {
    //     _formKey.currentState?.patchValue(
    //       {'code': widget.menuItem?.code},
    //     );
    //   });
    // }
  }

  Future initForm() async {
    categoryResult = await CategoryProvider().get();
    menuResult = await MenuProvider().get();
    print(categoryResult);
    print(menuResult);
    // setState(() async {
    //   categoryResult = await CategoryProvider().get();
    // });
    setState(() {
      isLoading = false;
    });
    print("widget.menuItem");
    print(widget.menuItem);
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      title: widget.menuItem?.name ?? "Product details",
      child: Column(
        children: [
          isLoading ? Container() : _buildForm(),
          Row(
            mainAxisAlignment: MainAxisAlignment.end,
            children: [
              Padding(
                padding: const EdgeInsets.all(10.0),
                child: ElevatedButton(
                    onPressed: () async {
                      _formKey.currentState?.saveAndValidate();

                      var request = new Map.from(_formKey.currentState!.value);
                      request['image'] = _base64Image;

                      try {
                        if (widget.menuItem == null) {
                          _menuItemProvider.create(request);
                        } else {
                          _menuItemProvider.update(widget.menuItem!.id!, request);
                        }
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
                    child: const Text("Sačuvaj")),
              )
            ],
          )
        ],
      ),
    );
  }

  FormBuilder _buildForm() {
    return FormBuilder(
      key: _formKey,
      initialValue: _initialValue,
      child: Column(
        children: [
          Row(
            children: [
              Expanded(
                child: FormBuilderTextField(
                  decoration: const InputDecoration(labelText: "Naziv"),
                  name: "name",
                ),
              ),
              const SizedBox(
                width: 10,
              ),
              Expanded(
                child: FormBuilderTextField(
                  decoration: const InputDecoration(labelText: "Šifra"),
                  name: "code",
                ),
              ),
            ],
          ),
          Row(
            children: [
              Expanded(
                child: FormBuilderTextField(
                  decoration: const InputDecoration(labelText: "Cijena(BAM)"),
                  name: "price",
                ),
              ),
              const SizedBox(
                width: 10,
              ),
              Expanded(
                child: FormBuilderTextField(
                  decoration: const InputDecoration(labelText: "Na stanju"),
                  name: "inStorage",
                ),
              ),
            ],
          ),
          Row(
            children: [
              Expanded(
                child: FormBuilderDropdown<String>(
                  name: 'categoryId',
                  decoration: InputDecoration(
                      labelText: "Kategorija",
                      suffix: IconButton(
                        icon: const Icon(
                          Icons.close,
                        ),
                        onPressed: () {
                          _formKey.currentState!.fields['categoryId']?.reset();
                        },
                      ),
                      hintText: "Odaberi kategoriju"),
                  items: categoryResult?.result
                          .map((item) => DropdownMenuItem(
                                alignment: AlignmentDirectional.center,
                                value: item.id != null ? item.id.toString() : "",
                                child: Text(item.name ?? ""),
                              ))
                          .toList() ??
                      [],
                ),
              ),
              SizedBox(width: 10),
              Expanded(
                child: FormBuilderDropdown<String>(
                  name: 'menuId',
                  decoration: InputDecoration(
                      labelText: "Meni",
                      suffix: IconButton(
                        icon: const Icon(
                          Icons.close,
                        ),
                        onPressed: () {
                          _formKey.currentState!.fields['menuId']?.reset();
                        },
                      ),
                      hintText: "Odaberi meni"),
                  items: menuResult?.result
                          .map((item) => DropdownMenuItem(
                                alignment: AlignmentDirectional.center,
                                value: item.id != null ? item.id.toString() : "",
                                child: Text(item.name ?? ""),
                              ))
                          .toList() ??
                      [],
                ),
              )
            ],
          ),
          FormBuilderTextField(
            decoration: const InputDecoration(labelText: "Opis"),
            name: "description",
          ),
          Row(
            children: [
              Expanded(
                child: FormBuilderField(
                  name: "imageId",
                  builder: _buildImagePicker,
                ),
              ),
            ],
          ),
          Column(
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
                child: widget.menuItem != null
                    ? imageFromBase64String(widget.menuItem?.image ?? "")
                    : imageFromBase64String(previewImage ?? ""),
              ),
            ],
          )
        ],
      ),
    );
  }

  Widget _buildImagePicker(field) {
    return InputDecorator(
        decoration: InputDecoration(
          label: Text("Slika"),
          errorText: field.errorText,
        ),
        child: ListTile(
          leading: Icon(Icons.photo),
          title: Text("Odaberite sliku"),
          trailing: Icon(Icons.file_upload),
          onTap: _getImage,
        ));
  }

  File? _image;
  String? _base64Image;

  Future _getImage() async {
    var result = await FilePicker.platform.pickFiles(type: FileType.image);
    if (result != null && result.files.single.path != null) {
      _image = File(result.files.single.path!);
      _base64Image = base64Encode(_image!.readAsBytesSync());
    }

    setState(() {
      widget.menuItem?.image = _base64Image;
      previewImage = _base64Image;
    });
  }
}
