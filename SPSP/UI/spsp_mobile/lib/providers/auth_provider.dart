import 'dart:convert';
import 'package:spsp_mobile/models/user_account.dart';
import 'package:spsp_mobile/models/user_auth_info.dart';
import 'package:spsp_mobile/providers/base_provider.dart';
import 'package:spsp_mobile/utils/util.dart';

class AuthProvider extends BaseProvider<UserAuthInfo> {
  AuthProvider() : super("Auth");

  @override
  UserAuthInfo fromJson(data) {
    return UserAuthInfo.fromJson(data);
  }
}
