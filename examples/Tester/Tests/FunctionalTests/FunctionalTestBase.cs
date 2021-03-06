﻿#region Copyright notice and license

// Copyright 2019 The gRPC Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Tests.FunctionalTests
{
    public class FunctionalTestBase
    {
        private GrpcChannel? _channel;

        protected GrpcTestFixture<Server.Startup> Fixture { get; private set; } = default!;

        protected ILoggerFactory LoggerFactory => Fixture.LoggerFactory;

        protected GrpcChannel Channel => _channel ??= CreateChannel();

        protected GrpcChannel CreateChannel()
        {
            return GrpcChannel.ForAddress(Fixture.Client.BaseAddress, new GrpcChannelOptions
            {
                LoggerFactory = LoggerFactory,
                HttpClient = Fixture.Client
            });
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Fixture = new GrpcTestFixture<Server.Startup>(ConfigureServices);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Fixture.Dispose();
        }
    }
}