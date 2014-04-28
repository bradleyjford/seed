(function (angular) {
    'use strict';

    var module = angular.module('seedApp.home');

    module.controller('SignInController', ['$scope', '$state', '$stateParams', '$location', 'AuthenticationApi', 'SecurityPrincipal',
        function ($scope, $state, $stateParams, $location, AuthenticationApi, SecurityPrincipal) {
            $scope.model = {
                username: '',
                password: '',
                error: ''
            };

            $scope.signIn = function () {
                $scope.model.error = '';

                if (!$scope.signInForm.$valid) {
                    return;
                }

                AuthenticationApi.signIn($scope.model.username, $scope.model.password)
                    .success(function (data) {
                        SecurityPrincipal.set(data.username, data.fullName, data.roles);

                        if ($stateParams.returnUrl && $stateParams.returnUrl.length > 0) {
                            $location.url($stateParams.returnUrl);
                            return;
                        }

                        $state.go('home');
                    })
                    .error(function (data, status, headers, config) {
                        $scope.model.error = data.message;
                    });
            };
        }
    ]);
})(angular);