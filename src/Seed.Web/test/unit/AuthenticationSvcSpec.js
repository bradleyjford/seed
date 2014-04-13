describe('AuthenticationSvc', function () {
    'use strict';

    var $httpBackend;

    beforeEach(module('seedApp.security'));

    beforeEach(inject(function (_$httpBackend_) {
        $httpBackend = _$httpBackend_;
    }));

    afterEach(function () {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });
/*
    it('sets isAuthenticated to true following successful sign in', inject(function (AuthenticationSvc) {
        $httpBackend.when('POST', '/api/authentication/signin')
            .respond(200, 'OK');

        AuthenticationSvc.signIn('test', 'test');

        $httpBackend.flush();

        expect(AuthenticationSvc.isAuthenticated()).toEqual(true);
    })); 

    it('sets isAuthenticated to false following failed sign in', inject(function (AuthenticationSvc) {
        $httpBackend.when('POST', '/api/authentication/signin')
            .respond(401, 'Unauthorized');

        AuthenticationSvc.signIn('test', 'test');

        $httpBackend.flush();

        expect(AuthenticationSvc.isAuthenticated()).toEqual(false);
    }));
*/
});
