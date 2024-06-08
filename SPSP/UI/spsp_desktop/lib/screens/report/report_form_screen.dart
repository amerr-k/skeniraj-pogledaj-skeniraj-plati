// ignore_for_file: non_constant_identifier_names
// ignore_for_file: prefer_const_constructors, prefer_const_literals_to_create_immutables
import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:form_builder_validators/form_builder_validators.dart';
import 'package:provider/provider.dart';
import 'package:spsp_desktop/models/customer_report_data/customer_report_data.dart';
import 'package:spsp_desktop/models/enums/ReportParameter.dart';
import 'package:spsp_desktop/models/invoice/invoice.dart';
import 'package:spsp_desktop/models/menu/menu.dart';
import 'package:spsp_desktop/models/menu_item_report_data/menu_item_report_data.dart';
import 'package:spsp_desktop/models/search_result.dart';
import 'package:spsp_desktop/models/invoice/supplier.dart';
import 'package:spsp_desktop/pdf_utils/pdf_api.dart';
import 'package:spsp_desktop/pdf_utils/pdf_customer_report_api.dart';
import 'package:spsp_desktop/pdf_utils/pdf_menu_item_report_api.dart';
import 'package:spsp_desktop/providers/report_provider.dart';
import 'package:spsp_desktop/widgets/master_screen.dart';

class ReportFormScreen extends StatefulWidget {
  ReportFormScreen({super.key});
  @override
  State<ReportFormScreen> createState() => _ReportFormScreenState();
}

class _ReportFormScreenState extends State<ReportFormScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  late ReportProvider _reportProvider;

  Map<String, dynamic> _initialValue = {};
  RequestResult<Menu>? menuResult;
  bool isLoading = true;
  List<MenuItemReportData> menuItemReportData = [];
  List<CustomerReportData> customerReportData = [];

  var BUSSINESS_NAME = String.fromEnvironment('BUSSINESS_NAME_VALUE', defaultValue: 'Caffe Pub - Skeniraj Plati');
  final BUSSINESS_ADDRESS =
      String.fromEnvironment('BUSSINESS_ADDRESS_VALUE', defaultValue: 'ul. Abdulaha Sidrana, Sarajevo, BiH');
  final BUSSINESS_CONTACT_INFO =
      String.fromEnvironment('BUSSINESS_CONTACT_INFO_VALUE', defaultValue: '+387 62 123 321');

  @override
  void initState() {
    super.initState();
    _initialValue = {};

    _reportProvider = context.read<ReportProvider>();

    initForm();
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
  }

  initForm() async {
    setState(() {
      isLoading = false;
    });
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      title: "Izvještaji",
      child: Center(
        child: Container(
          constraints: const BoxConstraints(maxWidth: 400, maxHeight: 500),
          child: Card(
            child: Padding(
              padding: EdgeInsets.all(16.0),
              child: Column(
                children: [
                  Expanded(
                    child: _buildForm(),
                  ),
                  _buildGenerateButton(),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }

  Row _buildGenerateButton() {
    return Row(
      mainAxisAlignment: MainAxisAlignment.end,
      children: [
        Padding(
          padding: const EdgeInsets.all(10.0),
          child: ElevatedButton(
              onPressed: () async {
                var isValid = _formKey.currentState?.saveAndValidate() ?? false;
                if (isValid) {
                  var request = _formKey.currentState!.value;

                  try {
                    final invoice = Invoice(
                      supplier: Supplier(
                          name: BUSSINESS_NAME, address: BUSSINESS_ADDRESS, contactInfo: BUSSINESS_CONTACT_INFO),
                    );

                    switch (request['subject']) {
                      case 'MENU_ITEM':
                        menuItemReportData = await _reportProvider.getMenuItemsReportData(filter: request);

                        await _generateMenuItemsReport(invoice, menuItemReportData, request['numberOfResults'],
                            request['withDetails'], request['withSum']);

                        break;
                      case 'CUSTOMER':
                        customerReportData = await _reportProvider.getCustomersReportData(filter: request);

                        await _generateCustomersReport(invoice, customerReportData, request['numberOfResults'],
                            request['withDetails'], request['withSum']);

                        break;
                    }
                  } on Exception catch (e) {
                    showDialog(
                      context: context,
                      builder: ((BuildContext context) => AlertDialog(
                            title: const Text("Error"),
                            content: Text(
                              e.toString(),
                            ),
                            actions: [
                              TextButton(
                                onPressed: () => Navigator.pop(context),
                                child: const Text("Uredu"),
                              ),
                            ],
                          )),
                    );
                  }
                }
              },
              child: const Text("Generiši izvještaj")),
        )
      ],
    );
  }

  FormBuilder _buildForm() {
    return FormBuilder(
      key: _formKey,
      initialValue: _initialValue,
      child: Column(
        children: [
          FormBuilderDropdown<String>(
            name: 'quarter',
            decoration: InputDecoration(
                labelText: "Kvartal",
                suffix: IconButton(
                  icon: const Icon(
                    Icons.close,
                  ),
                  onPressed: () {
                    _formKey.currentState!.fields['quarter']?.reset();
                  },
                ),
                hintText: "Odaberi kategoriju"),
            items: [
              DropdownMenuItem<String>(
                value: ReportQuarterParameter.Q1.name,
                child: Text(ReportQuarterParameter.Q1.value),
              ),
              DropdownMenuItem<String>(
                value: ReportQuarterParameter.Q2.name,
                child: Text(ReportQuarterParameter.Q2.value),
              ),
              DropdownMenuItem<String>(
                value: ReportQuarterParameter.Q3.name,
                child: Text(ReportQuarterParameter.Q3.value),
              ),
              DropdownMenuItem<String>(
                value: ReportQuarterParameter.Q4.name,
                child: Text(ReportQuarterParameter.Q4.value),
              ),
            ],
          ),
          FormBuilderDropdown<String>(
            validator: FormBuilderValidators.required(errorText: "Polje je obavezno."),
            name: 'subject',
            decoration: InputDecoration(
                labelText: "Predmet",
                suffix: IconButton(
                  icon: const Icon(
                    Icons.close,
                  ),
                  onPressed: () {
                    _formKey.currentState!.fields['subject']?.reset();
                  },
                ),
                hintText: "Predmet po kojem želite kreiran izvještaj."),
            items: [
              DropdownMenuItem<String>(
                value: ReportSubjectParameter.CUSTOMER.name,
                child: Text(ReportSubjectParameter.CUSTOMER.value),
              ),
              DropdownMenuItem<String>(
                value: ReportSubjectParameter.MENU_ITEM.name,
                child: Text(ReportSubjectParameter.MENU_ITEM.value),
              ),
            ],
          ),
          FormBuilderDropdown<int>(
            validator: FormBuilderValidators.required(errorText: "Polje je obavezno."),
            name: 'numberOfResults',
            decoration: InputDecoration(
                labelText: "Broj stavki",
                suffix: IconButton(
                  icon: const Icon(
                    Icons.close,
                  ),
                  onPressed: () {
                    _formKey.currentState!.fields['numberOfResults']?.reset();
                  },
                ),
                hintText: "Broj rezultata koje će izvještaj vratiti ukoliko podaci postoje."),
            items: const [
              DropdownMenuItem<int>(
                value: 1,
                child: Text("1"),
              ),
              DropdownMenuItem<int>(
                value: 5,
                child: Text("5"),
              ),
              DropdownMenuItem<int>(
                value: 10,
                child: Text("10"),
              ),
              DropdownMenuItem<int>(
                value: 15,
                child: Text("15"),
              ),
              DropdownMenuItem<int>(
                value: 20,
                child: Text("20"),
              ),
            ],
          ),
          FormBuilderDropdown<bool>(
            validator: FormBuilderValidators.required(errorText: "Polje je obavezno."),
            initialValue: false,
            name: 'withDetails',
            decoration: InputDecoration(
                labelText: "Uključiti širi set informacija za odabrane podatke",
                // suffix: IconButton(
                //   icon: const Icon(
                //     Icons.close,
                //   ),
                //   onPressed: () {
                //     _formKey.currentState!.fields['details']?.reset();
                //   },
                // ),
                hintText: "Uključen širi set podataka."),
            items: const [
              DropdownMenuItem<bool>(
                value: true,
                child: Text("Da"),
              ),
              DropdownMenuItem<bool>(
                value: false,
                child: Text("Ne"),
              ),
            ],
          ),
          FormBuilderDropdown<bool>(
            validator: FormBuilderValidators.required(errorText: "Polje je obavezno."),
            initialValue: false,
            name: 'withSum',
            decoration: InputDecoration(
                labelText: "Uključiti izračunutu ukupna sumu prihoda za odabrane podatke",
                // suffix: IconButton(
                //   icon: const Icon(
                //     Icons.close,
                //   ),
                //   onPressed: () {
                //     _formKey.currentState!.fields['details']?.reset();
                //   },
                // ),
                hintText: "Uključena izračunutu ukupna sumu prihoda odabranog izvještaja."),
            items: const [
              DropdownMenuItem<bool>(
                value: true,
                child: Text("Da"),
              ),
              DropdownMenuItem<bool>(
                value: false,
                child: Text("Ne"),
              ),
            ],
          ),
        ],
      ),
    );
  }

  Future _generateCustomersReport(Invoice invoice, List<CustomerReportData> customerReportData, int numberOfResults,
      bool withDetails, bool withSum) async {
    final pdfFile =
        await PdfCustomerReportApi.generateAsFile(invoice, customerReportData, numberOfResults, withDetails, withSum);

    PdfApi.openFile(pdfFile);
  }

  Future _generateMenuItemsReport(Invoice invoice, List<MenuItemReportData> menuItemReportData, int numberOfResults,
      bool withDetails, bool withSum) async {
    final pdfFile =
        await PdfMenuItemReportApi.generateAsFile(invoice, menuItemReportData, numberOfResults, withDetails, withSum);

    PdfApi.openFile(pdfFile);
  }
}
