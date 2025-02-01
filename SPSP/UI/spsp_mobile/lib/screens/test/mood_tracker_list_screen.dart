import 'package:flutter/material.dart';
import 'package:flutter_form_builder/flutter_form_builder.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';
import 'package:spsp_mobile/models/customer.dart';
import 'package:spsp_mobile/models/enums/MoodTrackerStatus.dart';
import 'package:spsp_mobile/models/mood_tracker/mood_tracker.dart';
import 'package:spsp_mobile/models/search_result.dart';
import 'package:spsp_mobile/providers/customer_provider.dart';
import 'package:spsp_mobile/providers/mood_tracker_provider.dart';
import 'package:spsp_mobile/screens/test/mood_tracker_details_screen.dart';
import 'package:spsp_mobile/utils/util.dart';
import 'package:spsp_mobile/widgets/master_screen.dart';

class MoodTrackerListScreen extends StatefulWidget {
  const MoodTrackerListScreen({super.key});

  @override
  State<MoodTrackerListScreen> createState() => _MoodTrackerListScreenState();
}

class _MoodTrackerListScreenState extends State<MoodTrackerListScreen> {
  final _formKey = GlobalKey<FormBuilderState>();
  Map<String, dynamic> _initialValue = {};
  RequestResult<MoodTracker>? moodTrackers;
  RequestResult<Customer>? customers;
  late MoodTrackerProvider _moodTrackerProvider;
  late CustomerProvider _customerProvider;

  bool isLoading = true;
  bool isSearchVisible = false;

  @override
  void initState() {
    super.initState();

    _moodTrackerProvider = context.read<MoodTrackerProvider>();
    _customerProvider = context.read<CustomerProvider>();
    _initialValue = {};
    _loadData();
  }

  _loadData() async {
    var moodTrackersGetSearchResult = await _moodTrackerProvider.get();
    var customersGetSearchResult = await _customerProvider.get();
    setState(() {
      moodTrackers = moodTrackersGetSearchResult;
      customers = customersGetSearchResult;
      isLoading = false;
    });
  }

  @override
  void didChangeDependencies() async {
    super.didChangeDependencies();

    _moodTrackerProvider = context.read<MoodTrackerProvider>();
    var moodTrackersGetSearchResult = await _moodTrackerProvider.get();
    setState(() {
      moodTrackers = moodTrackersGetSearchResult;
    });
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      title: "MoodTracker",
      child: Column(
        children: [
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Container(
                padding: EdgeInsets.symmetric(horizontal: 10, vertical: 10),
                child: ElevatedButton(
                  onPressed: () async {
                    setState(() {
                      isSearchVisible = !isSearchVisible;
                    });
                  },
                  child: const Row(mainAxisAlignment: MainAxisAlignment.spaceBetween, children: [
                    Padding(padding: EdgeInsets.symmetric(horizontal: 10, vertical: 10), child: Text("Filteri")),
                    Icon(Icons.filter_list),
                  ]),
                ),
              ),
              Container(
                padding: EdgeInsets.symmetric(horizontal: 10, vertical: 10),
                child: ElevatedButton(
                  onPressed: () async {
                    Navigator.of(context).push(
                      MaterialPageRoute(
                        builder: (context) => MoodTrackerDetailsScreen(),
                      ),
                    );
                  },
                  child: const Row(mainAxisAlignment: MainAxisAlignment.spaceBetween, children: [
                    Padding(
                        padding: EdgeInsets.symmetric(horizontal: 10, vertical: 10),
                        child: Text("Kreiraj moodTracker")),
                    Icon(Icons.add),
                  ]),
                ),
              ),
            ],
          ),
          if (isSearchVisible) Expanded(child: _buildSearch()),
          if (isSearchVisible) Divider(),
          Expanded(
              child: isLoading
                  ? Container(
                      child: Center(child: CircularProgressIndicator()),
                    )
                  : _buildMoodTrackerCardList()),
        ],
      ),
    );
  }

  Widget _buildMoodTrackerCardList() {
    return ListView.builder(
      itemCount: moodTrackers?.count,
      itemBuilder: (context, index) {
        return _buildMoodTrackerCard(moodTrackers!.result[index]);
      },
    );
  }

  Widget _buildMoodTrackerCard(MoodTracker item) {
    return ListTile(
      onTap: () {
        Navigator.of(context).push(
          MaterialPageRoute(
            builder: (context) => MoodTrackerDetailsScreen(
              moodTracker: item,
            ),
          ),
        );
      },
      leading: Text(item.customer?.phone != null ? "${item.moodTrackerStatus}" : ""),
      title: Text(item.customer?.phone != null
          ? "${item.customer?.userAccount?.firstName} ${item.customer?.userAccount?.lastName}"
          : ""),
      trailing: Text("${Utils.formatDate(item.datumEvidencije!)}"),
      subtitle: Text(item.customer?.phone != null ? "${item.dodatniOpis}" : ""),
    );
  }

  Widget _buildSearch() {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 4.0, horizontal: 8.0),
      child: SingleChildScrollView(
        child: FormBuilder(
          key: _formKey,
          initialValue: _initialValue,
          child: Column(
            children: [
              FormBuilderDateTimePicker(
                name: "datumEvidencijeSearch",
                decoration: InputDecoration(
                  labelText: "Datum evidencije",
                  suffixIcon: IconButton(
                    icon: const Icon(Icons.close),
                    onPressed: () {
                      setState(() {
                        _formKey.currentState?.fields['datumEvidencijeSearch']?.reset();
                      });
                    },
                  ),
                ),
                initialDate: DateTime.now(),
                firstDate: DateTime(2000),
                inputType: InputType.date,
                format: DateFormat('dd.MM.yyyy'),
                onChanged: (value) {},
                onSaved: (value) {},
              ),
              FormBuilderDropdown<MoodTrackerStatusEnum>(
                name: 'moodTrackerStatusEnum',
                decoration: InputDecoration(
                    labelText: "Status",
                    suffixIcon: IconButton(
                      icon: const Icon(
                        Icons.close,
                      ),
                      onPressed: () {
                        _formKey.currentState!.fields['moodTrackerStatusEnum']?.reset();
                      },
                    ),
                    hintText: "Odaberi status"),
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
                    value: MoodTrackerStatusEnum.EXCITED,
                    child: Text(MoodTrackerStatusEnum.EXCITED.value),
                  ),
                  DropdownMenuItem<MoodTrackerStatusEnum>(
                    value: MoodTrackerStatusEnum.STRESSED,
                    child: Text(MoodTrackerStatusEnum.STRESSED.value),
                  ),
                  DropdownMenuItem<MoodTrackerStatusEnum>(
                    value: MoodTrackerStatusEnum.TIRED,
                    child: Text(MoodTrackerStatusEnum.TIRED.value),
                  ),
                ],
              ),
              FormBuilderDropdown<String>(
                name: 'customerId',
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
              Padding(
                padding: const EdgeInsets.all(8.0),
                child: ElevatedButton(
                  onPressed: () async {
                    _formKey.currentState?.saveAndValidate();
                    var request = Map.from(_formKey.currentState!.value);

                    if (request['moodTrackerStatusEnum'] != null) {
                      var moodTrackerStatus = request['moodTrackerStatusEnum'] as MoodTrackerStatusEnum;
                      request['moodTrackerStatusEnum'] = moodTrackerStatus.index;
                    }

                    var moodTrackersSearchResult = await _moodTrackerProvider.get(filter: request);
                    setState(() {
                      moodTrackers = moodTrackersSearchResult!;
                    });
                  },
                  child: const Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [Text("Traži")],
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
