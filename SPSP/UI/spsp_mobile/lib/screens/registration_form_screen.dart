import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:form_builder_validators/form_builder_validators.dart';
import 'package:provider/provider.dart';
import 'package:spsp_mobile/models/enums/OrderStatus.dart';

import 'package:spsp_mobile/providers/auth_provider.dart';

import 'package:spsp_mobile/widgets/master_screen.dart';

class RegistrationFormScreenScreen extends StatefulWidget {
  // MenuItem? menuItem;
  // String? id;
  static const String routeName = "/registration";

  RegistrationFormScreenScreen({super.key});
  @override
  State<RegistrationFormScreenScreen> createState() =>
      _RegistrationFormScreenScreenState();
}

class _RegistrationFormScreenScreenState extends State<RegistrationFormScreenScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  Map<String, dynamic> _initialValue = {};
  late AuthProvider _authProvider;
  // late MenuItemProvider _menuItemProvider;
  // late MenuItemPredictionProvider _menuItemPredictionProvider;
  // RequestResult<Category>? categoryResult;
  // bool isLoading = true;
  // String? previewImage;
  // MenuItem? menuItemRequestResult;
  // List<MenuItemPrediction>? menuItemPredictions;

  @override
  void initState() {
    super.initState();
    // _initialValue = {};
    // _initialValue = {
    //   'code': menuItemRequestResult?.code,
    //   'name': menuItemRequestResult?.name,
    //   'description': menuItemRequestResult?.description,
    //   'price': menuItemRequestResult?.price?.toString(),
    // };

    _authProvider = context.read<AuthProvider>();
    // _menuProvider = context.read<MenuProvider>();
    // _menuItemPredictionProvider = context.read<MenuItemPredictionProvider>();

    initForm();
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
  }

  Future<void> initForm() async {
    // if (widget.id != null) {
    //   var result = await MenuItemProvider().getById(widget.id!);

    //   var recommendedMenuItemsResult =
    //       await MenuItemPredictionProvider().getByMainMenuItemId(widget.id!);

    //   if (widget.id != null) {
    //     var result = await MenuItemProvider().getById(widget.id!);
    //     setState(() {
    //       menuItemRequestResult = result;
    //       isLoading = false;
    //       _initialValue = {
    //         'code': menuItemRequestResult?.code,
    //         'name': menuItemRequestResult?.name,
    //         'description': menuItemRequestResult?.description,
    //         'price': menuItemRequestResult?.price?.toString(),
    //         'inStorage': menuItemRequestResult?.inStorage?.toString(),
    //         'category': menuItemRequestResult?.category?.name.toString(),
    //         'menuId': menuItemRequestResult?.menuId?.toString(),
    //         'image': menuItemRequestResult?.image,
    //       };
    //     });
    //   }

    //   setState(() {
    //     menuItemRequestResult = result;
    //     menuItemPredictions = recommendedMenuItemsResult.result;
    //     isLoading = false;
    //   });
    // }
  }

  Widget build(BuildContext context) {
    return MasterScreenWidget(
      title: "Registracija",
      child: SingleChildScrollView(
        child: Column(
          children: [
            _buildForm(),
            Row(
              mainAxisAlignment: MainAxisAlignment.end,
              children: [
                Padding(
                  padding: const EdgeInsets.all(10.0),
                  child: ElevatedButton(
                      onPressed: () async {
                        _formKey.currentState?.saveAndValidate();
                        var request = new Map.from(_formKey.currentState!.value);

                        try {
                          await _authProvider.register(request);

                          Navigator.pop(context);
                          ScaffoldMessenger.of(context).showSnackBar(
                            const SnackBar(
                              content: Text(
                                "Uspješno ste kreirali račun",
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
                      },
                      child: const Text("Kreiraj račun")),
                )
              ],
            ),
          ],
        ),
      ),
    );
  }

  SingleChildScrollView _buildForm() {
    return SingleChildScrollView(
      padding: EdgeInsets.all(16.0),
      child: FormBuilder(
        key: _formKey,
        initialValue: _initialValue,
        autovalidateMode: AutovalidateMode.onUserInteraction,
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            FormBuilderTextField(
              decoration: const InputDecoration(
                  labelText: "Ime", labelStyle: TextStyle(color: Colors.black)),
              name: "firstName",
              style: const TextStyle(color: Colors.black),
              validator: FormBuilderValidators.required(
                  errorText: "Polje ne smije biti prazno."),
            ),
            SizedBox(height: 10),
            FormBuilderTextField(
              decoration: InputDecoration(
                labelText: "Prezime",
                labelStyle: TextStyle(color: Colors.black),
              ),
              name: "lastName",
              style: TextStyle(color: Colors.black),
            ),
            SizedBox(height: 10),
            FormBuilderTextField(
              decoration: const InputDecoration(
                  labelText: "E-mail", labelStyle: TextStyle(color: Colors.black)),
              name: "email",
              style: const TextStyle(color: Colors.black),
              validator: FormBuilderValidators.email(
                  errorText: "Unesite ispravnu e-mail adresu."),
            ),
            SizedBox(height: 10),
            FormBuilderTextField(
              decoration: const InputDecoration(
                  labelText: "Korisničko ime",
                  labelStyle: TextStyle(color: Colors.black)),
              name: "username",
              style: const TextStyle(color: Colors.black),
              validator: FormBuilderValidators.required(
                  errorText: "Polje ne smije biti prazno."),
            ),
            SizedBox(height: 10),
            FormBuilderTextField(
              decoration: const InputDecoration(
                  labelText: "Lozinka", labelStyle: TextStyle(color: Colors.black)),
              name: "password",
              obscureText: true,
              style: const TextStyle(color: Colors.black),
              validator: FormBuilderValidators.required(
                  errorText: "Polje ne smije biti prazno."),
            ),
            SizedBox(height: 10),
            FormBuilderTextField(
                decoration: const InputDecoration(
                    labelText: "Ponovite lozinku",
                    labelStyle: TextStyle(color: Colors.black)),
                name: "passwordRepeat",
                validator: FormBuilderValidators.compose([
                  FormBuilderValidators.required(
                      errorText: "Polje ne smije biti prazno."),
                  (passwordRepeatValue) {
                    var password = _formKey.currentState?.fields["password"]!.value;
                    if (passwordRepeatValue == password) return null;
                    return 'Lozinke se ne podudaraju';
                  },
                ]),
                obscureText: true,
                style: const TextStyle(color: Colors.black)),
          ],
        ),
      ),
    );
  }
}
