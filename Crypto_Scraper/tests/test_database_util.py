import unittest
import utils.database

class TestDatabaseUtils(unittest.TestCase):

    def setUp(self):
        self.db_file = ":memory:"
        self.conn = utils.database.create_connection(self.db_file)

    def test_connection_connected(self):
        self.assertTrue(self.conn)

if __name__ == '__main__':
    unittest.main()
