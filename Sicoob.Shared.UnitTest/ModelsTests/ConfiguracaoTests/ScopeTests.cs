/**************************************\
 * Biblioteca C# para APIs do SICOOB  *
 * Autor: Rafael Estevam              *
 *        gh/SharpSistemas/SicoobAPI  *
\**************************************/
using Sicoob.Shared.Models;
using Xunit;

namespace Sicoob.Shared.UnitTest.ModelsTests.ConfiguracaoTests
{
    public class ScopeTests
    {
        [Fact]
        public void Models_Configuration_Scope_None()
        {
            var scope = new AuthorizationScope();
            Assert.Empty(scope.ToScopeString());
        }
        [Fact]
        public void Models_Configuration_Scope_All()
        {
            var scope = new AuthorizationScope()
            {
                COB_WRITE = true,
                COB_READ = true,
                COBV_WRITE = true,
                COBV_READ = true,
                LOTE_COBV_WRITE = true,
                LOTE_COBV_READ = true,

                PIX_WRITE = true,
                PIX_READ = true,
                WEBHOOK_WRITE = true,
                WEBHOOK_READ = true,
                PAYLOAD_LOCATION_WRITE = true,
                PAYLOAD_LOCATION_READ = true,
            };
            Assert.Equal("cob.write cob.read cobv.write cobv.read lotecobv.write lotecobv.read pix.write pix.read webhook.write webhook.read payloadlocation.write payloadlocation.read", scope.ToScopeString());
        }
        [Fact]
        public void Models_Configuration_Scope_AllRead()
        {
            var scope = new AuthorizationScope()
            {
                COB_READ = true,
                COBV_READ = true,
                LOTE_COBV_READ = true,
                PIX_READ = true,
                WEBHOOK_READ = true,
                PAYLOAD_LOCATION_READ = true,
            };
            Assert.Equal("cob.read cobv.read lotecobv.read pix.read webhook.read payloadlocation.read", scope.ToScopeString());
        }
        [Fact]
        public void Models_Configuration_Scope_AllWrite()
        {
            var scope = new AuthorizationScope()
            {
                COB_WRITE = true,
                COBV_WRITE = true,
                LOTE_COBV_WRITE = true,

                PIX_WRITE = true,
                WEBHOOK_WRITE = true,
                PAYLOAD_LOCATION_WRITE = true,
            };
            Assert.Equal("cob.write cobv.write lotecobv.write pix.write webhook.write payloadlocation.write", scope.ToScopeString());
        }
        [Fact]
        public void Models_Configuration_Scope_Pix()
        {
            var scope = new AuthorizationScope()
            {
                PIX_WRITE = true,
                PIX_READ = true,
            };
            Assert.Equal("pix.write pix.read", scope.ToScopeString());
        }
        [Fact]
        public void Models_Configuration_Scope_Cob()
        {
            var scope = new AuthorizationScope()
            {
                COB_WRITE = true,
                COB_READ = true,
            };
            Assert.Equal("cob.write cob.read", scope.ToScopeString());
        }
        [Fact]
        public void Models_Configuration_Scope_CobV()
        {
            var scope = new AuthorizationScope()
            {
                COBV_WRITE = true,
                COBV_READ = true,
                LOTE_COBV_WRITE = true,
                LOTE_COBV_READ = true,
            };
            Assert.Equal("cobv.write cobv.read lotecobv.write lotecobv.read", scope.ToScopeString());
        }
        [Fact]
        public void Models_Configuration_Scope_PixHook()
        {
            var scope = new AuthorizationScope()
            {
                PIX_WRITE = true,
                PIX_READ = true,
                WEBHOOK_WRITE = true,
                WEBHOOK_READ = true,
            };
            Assert.Equal("pix.write pix.read webhook.write webhook.read", scope.ToScopeString());
        }
    }
}
