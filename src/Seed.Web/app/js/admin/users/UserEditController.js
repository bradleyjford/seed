(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin');

    module.controller('UserEditController', [
        '$scope', '$state', 'confirm', 'UsersApi', 'model',

        function ($scope, $state, confirm, UsersApi, model) {
            $scope.model = model;

            $scope.save = function (model) {
                if ($scope.editUserForm.$valid) {
                    model.$save({ userId: model.id }, function () {
                        $state.go('^', null, { reload: true });
                    });
                }
            };

            $scope.cancel = function() {
                $state.go('^');
            };

            $scope.enable = function (model) {
                confirm.show(
                    model,
                    'Enable {{ fullName }}?',
                    'Are you sure that you wish to enable the user "{{ fullName }}"?',
                    {
                        okButtonText: 'Yes, enable',
                        cancelButtonText: 'No, cancel'
                    })
                    .then(function () {
                        UsersApi.activate({ userId: $scope.model.id }, null, function () {
                            $scope.model.isActive = true;
                        });
                    });
            };

            $scope.disable = function (model) {
                confirm.show(
                    model,
                    'Disable user "{{ fullName }}"?',
                    'Are you sure that you wish to disable user "{{ fullName }}"?',
                    {
                        okButtonText: 'Yes, disable',
                        okButtonClass: 'btn-danger',
                        cancelButtonText: 'No, cancel'
                    })
                    .then(function () {
                        UsersApi.deactivate({ userId: $scope.model.id }, null, function () {
                            $scope.model.isActive = false;
                        });
                    });
            };
        }
    ]);
})(angular)
;