(function (angular) {
    'use strict';

    angular.module('seedApp.security')
        .controller('SignInCtrl', ['$scope', '$location', 'AuthenticationApi', 'AlertSvc',
            function ($scope, $location, AuthenticationApi, AlertSvc) {
                $scope.model = {
                    userName: '',
                    password: ''
                };

                $scope.signIn = function () {
                    if (!$scope.signin_form.$valid) {
                        return;
                    }

                    AuthenticationApi.signIn($scope.model.userName, $scope.model.password)
                        .success(function (data) {
                            $scope.user.signIn(data.userName, data.roles);

                            $location.path('/');
                        })
                        .error(function (data, status, headers, config) {
                            AlertSvc.danger($scope, data.message);
                        });
                };
            }
        ]);
})(angular);