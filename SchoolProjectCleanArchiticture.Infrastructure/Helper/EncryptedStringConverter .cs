using EntityFrameworkCore.EncryptColumn.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Infrastructure.Helper
{
    public class EncryptedStringConverter : ValueConverter<string, string>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public EncryptedStringConverter(IEncryptionProvider encryptionProvider)
            : base(v => encryptionProvider.Encrypt(v), v => encryptionProvider.Decrypt(v))
        {
            _encryptionProvider = encryptionProvider;
        }
    }
}
