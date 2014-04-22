(function (angular) {
    'use strict';

    var module = angular.module('seedApp.security');

    module.controller('SignInCtrl', ['$scope', '$state', 'AuthenticationApi', 'NotificationSvc',
        function ($scope, $state, AuthenticationApi, NotificationSvc) {
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

                        $state.go('home');
                    })
                    .error(function (data, status, headers, config) {
                        NotificationSvc.error(data.message);
                    });
            };
        }
    ]);
})(angular);