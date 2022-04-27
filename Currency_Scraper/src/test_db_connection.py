import unittest
import utilities.db_utilities


class TestDatabaseUtils(unittest.TestCase):

    def setUp(self):
        self.db_file = ":memory:"
        self.conn = utilities.db_utilities.create_connection(self.db_file)

    def test_connection_connected(self):
        self.assertTrue(self.conn)


if __name__ == '__main__':
    unittest.main()
