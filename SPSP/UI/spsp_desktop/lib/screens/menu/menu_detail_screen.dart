import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:form_builder_validators/form_builder_validators.dart';
import 'package:provider/provider.dart';
import 'package:spsp_desktop/models/menu/menu.dart';
import 'package:spsp_desktop/models/search_result.dart';
import 'package:spsp_desktop/providers/menu_provider.dart';
import 'package:spsp_desktop/screens/menu/menu_list_screen.dart';
import 'package:spsp_desktop/screens/menu_item/menu_item_detail_screen.dart';
import 'package:spsp_desktop/screens/menu_item/menu_item_list_screen.dart';
import 'package:spsp_desktop/widgets/master_screen.dart';

class MenuDetailScreen extends StatefulWidget {
  Menu? menu;

  MenuDetailScreen({super.key, this.menu});
  @override
  State<MenuDetailScreen> createState() => _MenuDetailScreenState();
}

class _MenuDetailScreenState extends State<MenuDetailScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  Map<String, dynamic> _initialValue = {};
  late MenuProvider _menuProvider;
  RequestResult<Menu>? menuResult;
  bool isLoading = true;
  String? previewImage;

  @override
  void initState() {
    super.initState();
    _initialValue = {
      'name': widget.menu?.name,
      'isActive': widget.menu?.isActive,
    };

    _menuProvider = context.read<MenuProvider>();

    initForm();
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
  }

  Future initForm() async {
    setState(() {
      isLoading = false;
    });
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      title: widget.menu?.name ?? "Meni detalji",
      child: Padding(
        padding: const EdgeInsets.all(12.0),
        child: Column(
          children: [
            isLoading ? Container() : _buildForm(),
            Row(
              mainAxisAlignment: MainAxisAlignment.end,
              children: [
                Padding(
                  padding: const EdgeInsets.all(10.0),
                  child: ElevatedButton(
                    onPressed: () => Navigator.pop(context),
                    child: const Text("Natrag"),
                  ),
                ),
                Padding(
                  padding: const EdgeInsets.all(10.0),
                  child: ElevatedButton(
                    onPressed: widget.menu != null
                        ? () => Navigator.of(context).push(
                              MaterialPageRoute(
                                builder: (context) => MenuItemListScreen(
                                  menuId: widget.menu!.id,
                                ),
                              ),
                            )
                        : null,
                    child: const Text("Uredi meni"),
                  ),
                ),
                Padding(
                  padding: const EdgeInsets.all(10.0),
                  child: ElevatedButton(
                      onPressed: () async {
                        var isValid = _formKey.currentState?.saveAndValidate() ?? false;
                        if (isValid) {
                          var request = new Map.from(_formKey.currentState!.value);
                          try {
                            if (widget.menu == null) {
                              await _menuProvider.create(request);
                            } else {
                              await _menuProvider.update(widget.menu!.id!, request);
                            }
                            Navigator.pushReplacement(
                                context,
                                MaterialPageRoute(
                                  builder: (context) => MenuListScreen(),
                                ));
                            ScaffoldMessenger.of(context).showSnackBar(
                              const SnackBar(
                                content: Text(
                                  "Uspješno ste sačuvali izmjene",
                                  style: TextStyle(color: Colors.white),
                                ),
                                backgroundColor: Colors.green,
                              ),
                            );
                          } on Exception catch (e) {
                            ScaffoldMessenger.of(context).showSnackBar(
                              SnackBar(
                                content: Text(
                                  e.toString(),
                                  style: TextStyle(color: Colors.white),
                                ),
                                backgroundColor: Colors.red,
                              ),
                            );
                          }
                        }
                      },
                      child: const Text("Sačuvaj")),
                )
              ],
            )
          ],
        ),
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
                  validator: FormBuilderValidators.required(errorText: "Polje ne smije biti prazno."),
                  decoration: const InputDecoration(labelText: "Naziv"),
                  name: "name",
                ),
              ),
              const SizedBox(
                width: 10,
              ),
              Expanded(
                child: FormBuilderSwitch(
                  enabled: widget.menu != null && widget.menu!.isActive == true ? false : true,
                  name: "isActive",
                  title: const Text("Meni aktivan"),
                  onChanged: (e) {
                    if (e != null && e) {
                      showDialog(
                        context: context,
                        builder: (BuildContext context) {
                          return AlertDialog(
                            title: const Text("Upozorenje"),
                            content: const Text("Aktivacijom menija, deaktivirate prethodno aktivni meni."),
                            actions: <Widget>[
                              TextButton(
                                child: const Text("Uredu"),
                                onPressed: () {
                                  Navigator.of(context).pop();
                                },
                              ),
                            ],
                          );
                        },
                      );
                    }
                  },
                ),
              ),
            ],
          ),
        ],
      ),
    );
  }
}
