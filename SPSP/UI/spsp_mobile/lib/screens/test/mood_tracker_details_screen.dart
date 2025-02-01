import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:form_builder_validators/form_builder_validators.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:spsp_mobile/models/customer.dart';
import 'package:spsp_mobile/models/enums/MoodTrackerStatus.dart';
import 'package:spsp_mobile/models/mood_tracker/mood_tracker.dart';
import 'package:spsp_mobile/models/search_result.dart';
import 'package:spsp_mobile/providers/customer_provider.dart';
import 'package:spsp_mobile/providers/mood_tracker_provider.dart';
import 'package:spsp_mobile/screens/test/mood_tracker_list_screen.dart';
import 'package:spsp_mobile/widgets/master_screen.dart';

class MoodTrackerDetailsScreen extends StatefulWidget {
  MoodTracker? moodTracker;
  MoodTrackerDetailsScreen({super.key, this.moodTracker});

  @override
  State<MoodTrackerDetailsScreen> createState() => _MoodTrackerDetailsScreenState();
}

class _MoodTrackerDetailsScreenState extends State<MoodTrackerDetailsScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  Map<String, dynamic> _initialValue = {};
  late CustomerProvider _customerProvider;
  late MoodTrackerProvider _moodTrackerProvider;
  bool isLoading = true;
  bool? istekao;
  RequestResult<Customer>? customers;

  @override
  void initState() {
    super.initState();
    _initialValue = {
      'dodatniOpis': widget.moodTracker?.dodatniOpis,
      'customerId': widget.moodTracker?.customerId.toString(),
      'datumPocetka': widget.moodTracker?.datumEvidencije,
      'moodTrackerStatusEnum':
          widget.moodTracker != null ? enumFromString(widget.moodTracker!.moodTrackerStatus) : null,
      'valid': widget.moodTracker?.valid,
    };
    _moodTrackerProvider = context.read<MoodTrackerProvider>();
    _customerProvider = context.read<CustomerProvider>();

    initForm();
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
  }

  Future<void> initForm() async {
    var customersResult = await CustomerProvider().get();

    setState(() {
      customers = customersResult;
      isLoading = false;
    });
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      title: "Detalji karte",
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
              ),
              Padding(
                padding: const EdgeInsets.all(10.0),
                child: ElevatedButton(
                    onPressed: () async {
                      var isValid = _formKey.currentState?.saveAndValidate() ?? false;
                      if (isValid) {
                        var request = Map.from(_formKey.currentState!.value);

                        if (request['datumEvidencije'] != null) {
                          var datumEvidencije = request['datumEvidencije'] as DateTime;
                          request['datumEvidencije'] = datumEvidencije.toIso8601String();
                        }

                        if (request['moodTrackerStatusEnum'] != null) {
                          var moodTrackerStatus = request['moodTrackerStatusEnum'] as MoodTrackerStatusEnum;
                          request['moodTrackerStatusEnum'] = moodTrackerStatus.index;
                        }

                        try {
                          if (widget.moodTracker == null) {
                            await _moodTrackerProvider.create(request);
                          } else {
                            await _moodTrackerProvider.update(widget.moodTracker!.id!, request);
                          }

                          Navigator.pushReplacement(
                            context,
                            MaterialPageRoute(
                              builder: (context) => MoodTrackerListScreen(),
                            ),
                          );
                          ScaffoldMessenger.of(context).showSnackBar(
                            const SnackBar(
                              content: Text(
                                "Uspješno ste kreirali aktivnost",
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
                    child: const Text("Snimi")),
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
            SizedBox(height: 10),
            FormBuilderDropdown<String>(
              name: 'customerId',
              validator: FormBuilderValidators.required(errorText: "Polje ne smije biti prazno."),
              decoration: InputDecoration(
                  labelText: "Kupac",
                  suffix: IconButton(
                    icon: const Icon(
                      Icons.close,
                    ),
                    onPressed: () {
                      _formKey.currentState!.fields['customerId']?.reset();
                    },
                  ),
                  hintText: "Odaberi kupca"),
              items: customers?.result
                      .map((item) => DropdownMenuItem(
                            alignment: AlignmentDirectional.center,
                            value: item.id != null ? item.id.toString() : "",
                            child: Text("${item.userAccount?.firstName ?? ""} ${item.userAccount?.lastName ?? ""}"),
                          ))
                      .toList() ??
                  [],
            ),
            const SizedBox(height: 10),
            FormBuilderTextField(
              decoration: const InputDecoration(labelText: "Dodatni opis", labelStyle: TextStyle(color: Colors.black)),
              name: "dodatniOpis",
              style: const TextStyle(color: Colors.black),
            ),
            const SizedBox(
              height: 10,
            ),
            FormBuilderDateTimePicker(
              enabled: true,
              name: "datumEvidencije",
              decoration: const InputDecoration(
                labelText: "Datum evidencije",
              ),
              // initialDate: DateTime.now(),
              // firstDate: DateTime.now(),
              inputType: InputType.date,
              format: DateFormat('dd.MM.yyyy'),
              onChanged: (value) async {},
              validator: FormBuilderValidators.required(errorText: "Polje ne smije biti prazno."),
            ),
            const SizedBox(
              height: 10,
            ),
            FormBuilderDropdown<MoodTrackerStatusEnum>(
              name: 'moodTrackerStatusEnum',
              validator: FormBuilderValidators.required(errorText: "Polje ne smije biti prazno."),
              decoration: InputDecoration(
                labelText: "Status narudžbe",
                suffixIcon: IconButton(
                  icon: const Icon(Icons.close),
                  onPressed: () {
                    _formKey.currentState!.fields['moodTrackerStatusEnum']?.reset();
                  },
                ),
                hintText: "Odaberi status aktivnosti",
              ),
              items: [
                DropdownMenuItem<MoodTrackerStatusEnum>(
                  value: MoodTrackerStatusEnum.HAPPY,
                  child: Text(MoodTrackerStatusEnum.HAPPY.value),
                ),
                DropdownMenuItem<MoodTrackerStatusEnum>(
                  value: MoodTrackerStatusEnum.SAD,
                  child: Text(MoodTrackerStatusEnum.SAD.value),
                ),
                DropdownMenuItem<MoodTrackerStatusEnum>(
                  value: MoodTrackerStatusEnum.STRESSED,
                  child: Text(MoodTrackerStatusEnum.STRESSED.value),
                ),
                DropdownMenuItem<MoodTrackerStatusEnum>(
                  value: MoodTrackerStatusEnum.EXCITED,
                  child: Text(MoodTrackerStatusEnum.EXCITED.value),
                ),
                DropdownMenuItem<MoodTrackerStatusEnum>(
                  value: MoodTrackerStatusEnum.TIRED,
                  child: Text(MoodTrackerStatusEnum.TIRED.value),
                ),
              ],
              onChanged: (value) {
                // handle the change if needed
              },
            ),
          ],
        ),
      ),
    );
  }
}
