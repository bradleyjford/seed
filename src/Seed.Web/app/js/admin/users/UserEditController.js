(function (angular) {
    'use strict';

    var module = angular.module('seedApp.admin');

    module.controller('UserEditController', [
        '$scope', '$state', 'confirm', 'UsersApi', 'model',

        function ($scope, $state, confirm, UsersApi, model) {
            var self = this;

            this.model = model;

            this.save = function (model) {
                if ($scope.editForm.$valid) {
                    model.$save({ userId: model.id }, function () {
                        $scope.editForm.$setPristine();

                        $state.go('^', null, { reload: true });
                    });
                }
            };

            this.cancel = function () {
                $scope.editForm.$setPristine();

                $state.go('^');
            };

            this.enable = function (model) {
                confirm.show(
                    model,
                    'Enable {{ fullName }}?',
                    'Are you sure that you wish to enable the user "{{ fullName }}"?',
                    {
                        okButtonText: 'Yes, enable',
                        cancelButtonText: 'No, cancel'
                    })
                    .then(function () {
                        UsersApi.activate({ userId: self.model.id }, null, function () {
                            $state.reload();
                        });
                    });
            };

            this.disable = function (model) {
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
                        UsersApi.deactivate({ userId: model.id }, null, function () {
                            $state.reload();
                        });
                    });
            };
        }
    ]);
})(angular);
