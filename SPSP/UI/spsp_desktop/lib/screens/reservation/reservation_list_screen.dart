import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:spsp_desktop/models/enums/ReservationStatus.dart';
import 'package:spsp_desktop/models/reservation/reservation.dart';
import 'package:spsp_desktop/models/search_result.dart';
import 'package:spsp_desktop/providers/reservation_provider.dart';
import 'package:spsp_desktop/utils/util.dart';
import 'package:spsp_desktop/widgets/master_screen.dart';

class ReservationListScreen extends StatefulWidget {
  const ReservationListScreen({super.key});

  @override
  State<ReservationListScreen> createState() => _ReservationListScreenState();
}

class _ReservationListScreenState extends State<ReservationListScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  Map<String, dynamic> _initialValue = {};
  RequestResult<Reservation>? searchResult;
  late ReservationProvider _reservationProvider;
  // final TextEditingController _ftsController = TextEditingController();

  @override
  void didChangeDependencies() async {
    super.didChangeDependencies();

    _reservationProvider = context.read<ReservationProvider>();

    var data = await _reservationProvider
        .get(filter: {"reservationStatus": ReservationStatus.CONFIRMED.name, "IsQRTableIncluded": true});
    setState(() {
      searchResult = data;
    });
  }

  @override
  void initState() {
    super.initState();

    _initialValue = {
      'reservationStatus': ReservationStatus.CONFIRMED.name.toString(),
    };
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      child: Container(
        child: Column(
          children: [_buildSearch(), _buildDataListView()],
        ),
      ),
    );
  }

  Widget _buildDataListView() {
    return Expanded(
      child: SingleChildScrollView(
        child: _buildDataTable(),
      ),
    );
  }

  Widget _buildDataTable() {
    return Row(
      children: [
        Expanded(
          child: DataTable(
              showCheckboxColumn: false,
              columnSpacing: 100,
              columns: const [
                DataColumn(
                  label: Expanded(
                    child: Text(
                      "Datum rezervacije",
                      style: TextStyle(fontStyle: FontStyle.italic),
                    ),
                  ),
                ),
                DataColumn(
                  label: Expanded(
                    child: Text(
                      'Broj stola',
                      style: TextStyle(fontStyle: FontStyle.italic),
                    ),
                  ),
                ),
                DataColumn(
                  label: Expanded(
                    child: Text(
                      'Opis stola',
                      style: TextStyle(fontStyle: FontStyle.italic),
                    ),
                  ),
                ),
                DataColumn(
                  label: Expanded(
                    child: Text(
                      'Broj stolica',
                      style: TextStyle(fontStyle: FontStyle.italic),
                    ),
                  ),
                ),
                DataColumn(
                  label: Expanded(
                    child: Text(
                      'Status',
                      style: TextStyle(fontStyle: FontStyle.italic),
                    ),
                  ),
                ),
              ],
              rows: searchResult?.result.asMap().entries.map(
                    (entry) {
                      var index = entry.key;
                      var e = entry.value;

                      return DataRow(
                        cells: [
                          DataCell(Text(Utils.formatDate(e.startTime!) ?? "")),
                          DataCell(e.qrTable != null ? Text(e.qrTable!.tableNumber.toString()) : const Text("")),
                          DataCell(e.qrTable != null ? Text(e.qrTable!.locationDescription) : const Text("")),
                          DataCell(e.qrTable != null ? Text(e.qrTable!.capacity.toString()) : const Text("")),
                          DataCell(Text(ReservationStatusExtension.enumFromString(e.status!).value)),
                        ],
                      );
                    },
                  ).toList() ??
                  []),
        ),
      ],
    );
  }

  Widget _buildSearch() {
    return Padding(
      padding: const EdgeInsets.all(8.0),
      child: FormBuilder(
        key: _formKey,
        initialValue: _initialValue,
        child: Row(
          children: [
            Expanded(
              child: FormBuilderDateTimePicker(
                name: "startTime",
                decoration: InputDecoration(
                    labelText: "Datum od",
                    suffixIcon: IconButton(
                      icon: const Icon(Icons.close),
                      onPressed: () {
                        setState(() {
                          _formKey.currentState?.fields['startTime']?.reset();
                        });
                      },
                    )),
                initialDate: DateTime.now(),
                inputType: InputType.date,
                format: DateFormat('dd.MM.yyyy'),
                onChanged: (value) {},
                onSaved: (value) {},
              ),
            ),
            SizedBox(
              width: 8,
            ),
            Expanded(
              child: FormBuilderDateTimePicker(
                name: "endTime",
                decoration: InputDecoration(
                    labelText: "Datum do",
                    suffixIcon: IconButton(
                      icon: const Icon(Icons.close),
                      onPressed: () {
                        setState(() {
                          _formKey.currentState?.fields['endTime']?.reset();
                        });
                      },
                    )),
                initialDate: DateTime.now(),
                inputType: InputType.date,
                format: DateFormat('dd.MM.yyyy'),
                onChanged: (value) {},
                onSaved: (value) {},
              ),
            ),
            SizedBox(
              width: 8,
            ),
            Expanded(
              child: FormBuilderDropdown<String>(
                name: 'reservationStatus',
                decoration: InputDecoration(
                    labelText: "Status rezervacije",
                    suffixIcon: IconButton(
                      icon: const Icon(
                        Icons.close,
                      ),
                      onPressed: () {
                        _formKey.currentState!.fields['orderStatus']?.reset();
                      },
                    ),
                    hintText: "Odaberi status rezervacije"),
                items: [
                  DropdownMenuItem<String>(
                    value: ReservationStatus.PENDING_CONFIRMATION.name,
                    child: Text(ReservationStatus.PENDING_CONFIRMATION.value),
                  ),
                  DropdownMenuItem<String>(
                    value: ReservationStatus.ON_HOLD.name,
                    child: Text(ReservationStatus.ON_HOLD.value),
                  ),
                  DropdownMenuItem<String>(
                    value: ReservationStatus.CONFIRMED.name,
                    child: Text(ReservationStatus.CONFIRMED.value),
                  ),
                  DropdownMenuItem<String>(
                    value: ReservationStatus.CANCELED.name,
                    child: Text(ReservationStatus.CANCELED.value),
                  ),
                ],
              ),
            ),
            SizedBox(
              width: 8,
            ),
            ElevatedButton(
              onPressed: () async {
                _formKey.currentState?.saveAndValidate();
                var request = Map.from(_formKey.currentState!.value);
                request['IsQRTableIncluded'] = true;
                var reservationListSearchResult = await _reservationProvider.get(filter: request);
                setState(() {
                  searchResult = reservationListSearchResult;
                });
              },
              child: const Text("Pretraga"),
            ),
          ],
        ),
      ),
    );
  }
}
