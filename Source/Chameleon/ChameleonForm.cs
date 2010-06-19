﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScintillaNet;
using ScintillaNet.Configuration;
using System.IO;
using Chameleon.GUI;
using Chameleon.Util;
using Chameleon.Network;
using SSHClient;

namespace Chameleon
{
	public partial class ChameleonForm : Form
	{
		private static bool m_appClosing = false;

		private Networking m_networking;
		private SSHProtocol m_sshProtocol;

		public static bool AppClosing
		{
			get { return m_appClosing; }
		}

		public ChameleonForm()
		{
			InitializeComponent();

			FormFontFixer.Fix(this);

			Options options = App.Configuration;

			/*
			string testSourcePath = "d:\\projects\\temp\\fizzbuzz.cpp";
			if(File.Exists(testSourcePath))
			{
				string source = File.ReadAllText(testSourcePath);
				m_editors.CurrentEditor.Text = source;
			}
			*/
			menuEditUndo.ShortcutKeyDisplayString = "Ctrl+Z";
			menuEditRedo.ShortcutKeyDisplayString = "Ctrl+Y";
			menuEditCopy.ShortcutKeyDisplayString = "Ctrl+C";
			menuEditCut.ShortcutKeyDisplayString = "Ctrl+X";
			menuEditPaste.ShortcutKeyDisplayString = "Ctrl+V";

			toolTextPassword.TextBox.UseSystemPasswordChar = true;

			m_networking = Networking.Instance;

			toolStatusConnected.Text = "Disconnected";

			toolTextHost.Text = "192.168.1.103";
			toolTextUser.Text = "root";

			toolHostDisconnect.Enabled = false;

			m_sshProtocol = new SSHProtocol();

			m_sshProtocol.OnDataIndicated += terminalEmulator1.IndicateData;
			//m_sshProtocol.OnDisconnect += this.OnDisconnected;
			terminalEmulator1.OnDataRequested += m_sshProtocol.RequestData;
			
		}

		private void menuHelpAbout_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Chameleon 2.0 alpha 0.0002");
		}

		private void ChameleonForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			m_appClosing = true;
			if(!m_editors.CloseAllFiles())
			{
				e.Cancel = true;
			}

			m_appClosing = false;

			App.Configuration.SaveSettings();
		}


		#region File Menu handlers
		private void OnNewFile(object sender, EventArgs e)
		{
			m_editors.NewFile();
		}

		private void OnFileOpenLocal(object sender, EventArgs e)
		{
			m_editors.OpenFile(FileLocation.Local);
		}

		private void OnFileOpenRemote(object sender, EventArgs e)
		{
			//m_editors.OpenFile(FileLocation.Remote);
			if(!m_networking.IsConnected)
			{
				MessageBox.Show("Can't do anything remote if not connected!");
				return;
			}

			m_editors.OpenFile(FileLocation.Remote);
			
		}

		private void OnFileSave(object sender, EventArgs e)
		{
			ChameleonEditor editor = m_editors.CurrentEditor;
			m_editors.SaveFile(editor, editor.FileLocation, false, true);
		}

		private void OnFileSaveAsLocal(object sender, EventArgs e)
		{
			ChameleonEditor editor = m_editors.CurrentEditor;
			m_editors.SaveFile(editor, FileLocation.Local, true, false);
		}

		private void OnFileSaveAsRemote(object sender, EventArgs e)
		{
			ChameleonEditor editor = m_editors.CurrentEditor;
			m_editors.SaveFile(editor, FileLocation.Remote, true, false);
		}

		private void OnFileClose(object sender, EventArgs e)
		{
			m_editors.CloseFile(m_editors.CurrentEditor);
		}

		private void OnFileCloseAll(object sender, EventArgs e)
		{
			m_editors.CloseAllFiles();
		}
		#endregion

		#region Edit Menu handlers
		private void menuEditUndo_Click(object sender, EventArgs e)
		{
			m_editors.CurrentEditor.UndoRedo.Undo();
		}

		private void menuEditRedo_Click(object sender, EventArgs e)
		{
			m_editors.CurrentEditor.UndoRedo.Redo();
		}

		private void menuEditCut_Click(object sender, EventArgs e)
		{
			m_editors.CurrentEditor.Clipboard.Cut();
		}

		private void menuEditCopy_Click(object sender, EventArgs e)
		{
			m_editors.CurrentEditor.Clipboard.Copy();
		}

		private void menuEditPaste_Click(object sender, EventArgs e)
		{
			m_editors.CurrentEditor.Clipboard.Paste();
		}

		private void menuEditFind_Click(object sender, EventArgs e)
		{
			m_editors.CurrentEditor.FindReplace.ShowFind();
		}

		private void menuEditReplace_Click(object sender, EventArgs e)
		{
			m_editors.CurrentEditor.FindReplace.ShowReplace();
		}

		#endregion

		private void toolHostConnect_Click(object sender, EventArgs e)
		{
			string host = toolTextHost.Text;
			string user = toolTextUser.Text;
			string password = toolTextPassword.Text;

			if(m_networking.Connect(host, user, password))
			{
				m_sshProtocol.Connect();

				toolTextHost.Enabled = false;
				toolTextUser.Enabled = false;
				toolTextPassword.Enabled = false;
				toolHostConnect.Enabled = false;
				toolHostDisconnect.Enabled = true;

				toolStatusConnected.Text = "Connected";
			}		
		}

		private void toolHostDisconnect_Click(object sender, EventArgs e)
		{
			m_networking.Disconnect();
			m_sshProtocol.Disconnect();
			

			toolTextHost.Enabled = true;
			toolTextUser.Enabled = true;
			toolTextPassword.Enabled = true;
			toolHostConnect.Enabled = true;
			toolHostDisconnect.Enabled = false;

			toolStatusConnected.Text = "Disconnected";
		}

	}
}
