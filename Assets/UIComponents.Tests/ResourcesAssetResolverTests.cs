﻿using NUnit.Framework;
using UIComponents.Core;

namespace UIComponents.Tests
{
    [TestFixture]
    public class ResourcesAssetResolverTests : AssetResolverTestSuite<ResourcesAssetResolver>
    {
        [Test]
        public void Should_Be_Able_To_Load_Existing_Asset()
        {
            Assert_Should_Be_Able_To_Load_Existing_Asset(
                "UIComponentTests/LayoutAttributeTests"
            );
        }

        [Test]
        public void Should_Be_Able_To_Tell_If_Asset_Exists()
        {
            Assert_Should_Be_Able_To_Tell_If_Asset_Exists(
                "UIComponentTests/LayoutAttributeTests"
            );
        }
    }
}