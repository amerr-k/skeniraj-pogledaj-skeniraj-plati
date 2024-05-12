import 'dart:convert';
import 'dart:io';

import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:spsp_desktop/models/category.dart';
import 'package:spsp_desktop/models/menu.dart';
import 'package:spsp_desktop/models/menu_item.dart';
import 'package:spsp_desktop/models/promotion.dart';
import 'package:spsp_desktop/models/search_result.dart';
import 'package:spsp_desktop/providers/category_provider.dart';
import 'package:spsp_desktop/providers/menu_item_provider.dart';
import 'package:spsp_desktop/providers/menu_provider.dart';
import 'package:spsp_desktop/providers/promotion_provider.dart';
import 'package:spsp_desktop/utils/util.dart';
import 'package:spsp_desktop/widgets/master_screen.dart';

class PromotionDetailScreen extends StatefulWidget {
  Promotion? promotion;

  PromotionDetailScreen({super.key, this.promotion});
  @override
  State<PromotionDetailScreen> createState() => _PromotionDetailScreenState();
}

class _PromotionDetailScreenState extends State<PromotionDetailScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  Map<String, dynamic> _initialValue = {};
  late MenuItemProvider _menuItemProvider;
  late PromotionProvider _promotionProvider;
  RequestResult<MenuItem>? menuItems;
  bool isLoading = true;

  @override
  void initState() {
    super.initState();
    _initialValue = {
      'description': widget.promotion?.description,
      'startTime': widget.promotion?.startTime,
      'endTime': widget.promotion?.endTime,
      'menuItemId': widget.promotion?.menuItemId.toString(),
      'active': widget.promotion?.active,
      // 'inStorage': widget.promotion?.inStorage?.toString(),
      // 'categoryId': widget.promotion?.categoryId?.toString(),
      // 'menuId': widget.promotion?.menuId?.toString(),
      // 'image': widget.promotion?.image,
    };

    _promotionProvider = context.read<PromotionProvider>();
    _menuItemProvider = context.read<MenuItemProvider>();

    initForm();
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
  }

  Future initForm() async {
    var menuItemGetSearchResult = await MenuItemProvider().get();

    setState(() {
      menuItems = menuItemGetSearchResult;
      isLoading = false;
    });
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      title: "Promocija artikla",
      child: Column(
        children: [
          isLoading ? Container() : _buildForm(),
          Row(
            mainAxisAlignment: MainAxisAlignment.end,
            children: [
              Padding(
                  padding: const EdgeInsets.all(10.0),
                  child: ElevatedButton(
                    child: const Text("Natrag"),
                    onPressed: () => Navigator.pop(context),
                  )),
              Padding(
                padding: const EdgeInsets.all(10.0),
                child: ElevatedButton(
                    onPressed: () async {
                      _formKey.currentState?.saveAndValidate();
                      var request = new Map.from(_formKey.currentState!.value);
                      var startTime = request['startTime'] as DateTime;
                      request['startTime'] = startTime.toIso8601String();
                      var endTime = request['endTime'] as DateTime;
                      request['endTime'] = endTime.toIso8601String();
                      try {
                        if (widget.promotion == null) {
                          await _promotionProvider.create(request);
                        } else {
                          await _promotionProvider.update(widget.promotion!.id!, request);
                        }
                        showDialog(
                          context: context,
                          builder: ((BuildContext context) => AlertDialog(
                                title: Text("Uspješno ste sačuvali izmjene"),
                                actions: [
                                  TextButton(
                                    onPressed: () => Navigator.pop(context),
                                    child: Text("Uredu"),
                                  ),
                                ],
                              )),
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
      child: Padding(
        padding: const EdgeInsets.all(10.0),
        child: Column(
          children: [
            FormBuilderTextField(
              decoration: const InputDecoration(labelText: "Opis"),
              name: "description",
            ),
            const SizedBox(
              height: 20,
            ),
            Row(
              children: [
                Expanded(
                  child: FormBuilderDateTimePicker(
                    name: "startTime",
                    decoration: const InputDecoration(
                      labelText: "Datum početka promocije",
                    ),
                    initialDate: DateTime.now(),
                    firstDate: DateTime.now(),
                    inputType: InputType.date,
                    format: DateFormat('dd.MM.yyyy'),
                    onChanged: (value) async {
                      if (value != null) {
                      } else {}
                    },
                    onSaved: (value) {},
                  ),
                ),
                const SizedBox(
                  width: 10,
                ),
                Expanded(
                  child: FormBuilderDateTimePicker(
                    name: "endTime",
                    decoration: const InputDecoration(
                      labelText: "Datum kraja promocije",
                    ),
                    initialDate: DateTime.now(),
                    firstDate: DateTime.now(),
                    inputType: InputType.date,
                    format: DateFormat('dd.MM.yyyy'),
                    onChanged: (value) async {
                      if (value != null) {
                      } else {}
                    },
                    onSaved: (value) {},
                  ),
                ),
              ],
            ),
            const SizedBox(
              height: 20,
            ),
            Row(
              children: [
                Expanded(
                  child: FormBuilderDropdown<String>(
                    name: 'menuItemId',
                    decoration: InputDecoration(
                        labelText: "Meni stavka",
                        suffix: IconButton(
                          icon: const Icon(
                            Icons.close,
                          ),
                          onPressed: () {
                            _formKey.currentState!.fields['menuItemId']?.reset();
                          },
                        ),
                        hintText: "Odaberi meni stavku"),
                    items: menuItems?.result
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
            const SizedBox(
              height: 20,
            ),
            Row(
              children: [
                Expanded(
                  child: FormBuilderSwitch(
                    // decoration: const InputDecoration(labelText: "Promocija aktivna"),
                    name: "active",
                    title: const Text("Promocija aktivna"),
                  ),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
