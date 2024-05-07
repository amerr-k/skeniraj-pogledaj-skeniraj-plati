// import 'package:flutter/material.dart';
// import 'package:flutter_form_builder/flutter_form_builder.dart';
// import 'package:intl/intl.dart';
// import 'package:spsp_mobile/models/qr_table.dart';
// import 'package:spsp_mobile/providers/cart_provider.dart';
// import 'package:spsp_mobile/widgets/qr_table_positions_widget.dart';

// class QRTableReservationDialogScreen extends StatelessWidget {
//   final List<QRTable> qrTableList;
//   final QRTableSelectorProvider qrTableSelectorProvider;
//   final void Function(VoidCallback) setState;
//   final void Function(String) updateFormField;
//   final void Function(String) updateStartTimeFormField;
//   final GlobalKey<FormBuilderState> formKey;

//   const QRTableReservationDialogScreen(
//       {super.key,
//       required this.qrTableList,
//       required this.qrTableSelectorProvider,
//       required this.setState,
//       required this.updateFormField,
//       required this.updateStartTimeFormField,
//       required this.formKey});

//   @override
//   Widget build(BuildContext context) => Dialog.fullscreen(
//         child: Center(
//           child: Column(
//             mainAxisSize: MainAxisSize.min,
//             // mainAxisAlignment: MainAxisAlignment.center,
//             children: [
//               Padding(
//                 padding: const EdgeInsets.all(10.0),
//                 child: Row(
//                   mainAxisAlignment: MainAxisAlignment.center,
//                   children: [
//                     Expanded(
//                       child: FormBuilderDateTimePicker(
//                         initialValue: qrTableSelectorProvider.reservationDate,
//                         name: "startTime",
//                         decoration: InputDecoration(
//                             labelText: "Datum rezervacije",
//                             suffixIcon: IconButton(
//                               icon: const Icon(Icons.close),
//                               onPressed: () {
//                                 // setState(() {
//                                 // formKey.currentState?.fields['startTime']?.reset();
//                                 // });
//                               },
//                             )),
//                         initialDate: DateTime.now(),
//                         firstDate: DateTime.now(),
//                         inputType: InputType.date, // Set inputType to date
//                         format: DateFormat('dd.MM.yyyy'),
//                         onChanged: (value) {
//                           if (value != null) {
//                             setState(() {
//                               qrTableSelectorProvider.setReservationDate(value);
//                             });
//                             updateStartTimeFormField("");
//                           } else {
//                             updateStartTimeFormField("");
//                           }
//                         },
//                         onSaved: (value) {},
//                       ),
//                     ),
//                   ],
//                 ),
//               ),
//               SizedBox(
//                 height: 30,
//               ),
//               const Row(
//                 mainAxisAlignment: MainAxisAlignment.center,
//                 children: [
//                   Text('ŠANK',
//                       style: TextStyle(
//                           fontStyle: FontStyle.italic, fontWeight: FontWeight.bold))
//                 ],
//               ),
//               const SizedBox(height: 25),
//               Row(
//                 mainAxisAlignment: MainAxisAlignment.center,
//                 children: [
//                   Transform.rotate(
//                     angle: -90 * 3.1415926535 / 180,
//                     child: const Text(
//                       'PROZORI',
//                       style: TextStyle(
//                           fontStyle: FontStyle.italic,
//                           fontWeight: FontWeight.bold,
//                           fontSize: 16.0),
//                     ),
//                   ),
//                   const SizedBox(width: 25),
//                   QRTablePositionsWidget(
//                     qrTableList: qrTableList,
//                     onTap: (tableNumber) {
//                       if (!qrTableList[tableNumber - 1].isReserved!) {
//                         // updateFormField(qrTableList[tableNumber - 1].id);

//                         // setState(() {
//                         //   qrTableSelectorProvider?.selectedQRTable =
//                         //       qrTableList[tableNumber - 1];
//                         // });

//                         Navigator.pop(context);
//                       }
//                     },
//                   ),
//                   const SizedBox(width: 25),
//                   Transform.rotate(
//                     angle: 90 * 3.1415926535 / 180,
//                     child: const Text(
//                       'TOALET',
//                       style: TextStyle(
//                         fontWeight: FontWeight.bold,
//                         fontSize: 16.0,
//                         fontStyle: FontStyle.italic,
//                       ),
//                     ),
//                   ),
//                 ],
//               ),
//               const SizedBox(height: 25),
//               Row(mainAxisAlignment: MainAxisAlignment.center, children: [
//                 ElevatedButton(
//                   onPressed: () async {
//                     Navigator.pop(context);
//                   },
//                   child: const Text("Zatvori"),
//                 )
//               ])
//             ],
//           ),
//         ),
//       );
// }
