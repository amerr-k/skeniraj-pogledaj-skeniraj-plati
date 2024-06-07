import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:spsp_desktop/models/menu/menu.dart';
import 'package:spsp_desktop/models/search_result.dart';
import 'package:spsp_desktop/providers/menu_provider.dart';
import 'package:spsp_desktop/screens/menu/menu_detail_screen.dart';
import 'package:spsp_desktop/widgets/master_screen.dart';

class MenuListScreen extends StatefulWidget {
  const MenuListScreen({super.key});

  @override
  State<MenuListScreen> createState() => _MenuItemListScreenState();
}

class _MenuItemListScreenState extends State<MenuListScreen> {
  RequestResult<Menu>? searchResult;
  late MenuProvider _menuProvider;
  final TextEditingController _nameController = TextEditingController();

  @override
  void didChangeDependencies() async {
    super.didChangeDependencies();

    _menuProvider = context.read<MenuProvider>();

    var data = await _menuProvider.get();
    setState(() {
      searchResult = data;
    });
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
              columns: const [
                DataColumn(
                  label: Expanded(
                    child: Text(
                      'Redni broj',
                      style: TextStyle(fontStyle: FontStyle.italic),
                    ),
                  ),
                ),
                DataColumn(
                  label: Expanded(
                    child: Text(
                      'Naziv',
                      style: TextStyle(fontStyle: FontStyle.italic),
                    ),
                  ),
                ),
              ],
              rows: searchResult?.result.asMap().entries.map(
                    (entry) {
                      int index = entry.key;
                      var e = entry.value;

                      return DataRow(
                        onSelectChanged: (selected) {
                          if (selected == true) {
                            Navigator.of(context).push(
                              MaterialPageRoute(
                                builder: (context) => MenuDetailScreen(
                                  menu: e,
                                ),
                              ),
                            );
                          }
                        },
                        cells: [
                          DataCell(Text((index + 1).toString())),
                          DataCell(Text(e.name ?? "")),
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
      child: Row(
        children: [
          Expanded(
            child: TextField(
              decoration: const InputDecoration(
                labelText: "Naziv",
              ),
              controller: _nameController,
            ),
          ),
          ElevatedButton(
            onPressed: () async {
              var data = await _menuProvider.get(filter: {
                'name': _nameController.text,
              });
              setState(() {
                searchResult = data;
              });
            },
            child: const Text("Pretraga"),
          ),
          const SizedBox(width: 8),
          ElevatedButton(
            onPressed: () async {
              Navigator.of(context).push(
                MaterialPageRoute(
                  builder: (context) => MenuDetailScreen(),
                ),
              );
            },
            child: const Text("Kreiraj novi meni"),
          ),
        ],
      ),
    );
  }
}
